using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace RaspberryApp.Classes
{
    public class HttpRequestHandler
    {
        private const int Port = 8081;
        private const uint BufferSize = 8192;

        private HtmlGenerator htmlGenerator;
        private Action handler;
        private StreamSocketListener listener;

        public HttpRequestHandler(HtmlGenerator htmlGenerator, Action handler)
        {
            this.htmlGenerator = htmlGenerator;
            this.handler = handler;
        }

        public async void Start()
        {
            listener = new StreamSocketListener();

            await listener.BindServiceNameAsync(Port.ToString());

            listener.ConnectionReceived += async (sender, args) =>
            {
                var request = new StringBuilder();

                using (var input = args.Socket.InputStream)
                {
                    var data = new byte[BufferSize];
                    var buffer = data.AsBuffer();
                    var bufferLength = BufferSize;

                    while (bufferLength == BufferSize)
                    {
                        await input.ReadAsync(buffer, BufferSize, InputStreamOptions.Partial);
                        request.Append(Encoding.UTF8.GetString(data, 0, data.Length));
                        bufferLength = buffer.Length;
                    }
                }

                var requestLines = request.ToString().Split(' ');
                var url = requestLines.Length > 1 ? requestLines[1] : string.Empty;
                var uri = new Uri("http://localhost" + url);
                var query = uri.Query;

                if (query.Contains("next"))
                {
                    handler();
                }

                using (var output = args.Socket.OutputStream)
                {
                    using (var response = output.AsStreamForWrite())
                    {
                        var html = htmlGenerator.GetHtml();
                        var bodyArray = Encoding.UTF8.GetBytes(html);
                        var bodyStream = new MemoryStream(bodyArray);
                        var header = $"HTTP/1.1 200 OK\r\nContent-Length: {bodyStream.Length}\r\nConnection: close\r\n\r\n";
                        var headerArray = Encoding.UTF8.GetBytes(header);

                        await response.WriteAsync(headerArray, 0, headerArray.Length);
                        await bodyStream.CopyToAsync(response);
                        await response.FlushAsync();
                    }
                }
            };
        }
    }
}