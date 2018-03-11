using System.Text;

namespace RaspberryApp.Classes
{
    public class HtmlGenerator
    {
        public string GetHtml()
        {
            var sb = new StringBuilder();

            sb.AppendLine(@"<!DOCTYPE html>");
            sb.AppendLine(@"<html>");
            sb.AppendLine(@"<head>");
            sb.AppendLine(@"<meta charset=""utf-8"">");
            sb.AppendLine(@"<meta name=""viewport"" content=""width=device-width, initial-scale=1"">");
            sb.AppendLine(@"<link rel=""stylesheet"" href=""https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"">");
            sb.AppendLine(@"<script src=""https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js""></script>");
            sb.AppendLine(@"<script src=""https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js""></script>");
            sb.AppendLine(@"</head>");
            sb.AppendLine(@"<body>");
            sb.AppendLine(@"<div class=""container"">");
            sb.AppendLine(@"<div class=""page-header"">");
            sb.AppendLine(@"<h1>Raspberry Pi</h1>");
            sb.AppendLine(@"<h2>Sense Hat demo</h2>");
            sb.AppendLine(@"</div>");
            sb.AppendLine(@"<p><a href=""/?next"" class=""btn btn-link"">Next action</a></p>");
            sb.AppendLine(@"</div>");
            sb.AppendLine(@"</body>");
            sb.AppendLine(@"</html>");

            return sb.ToString();
        }
    }
}