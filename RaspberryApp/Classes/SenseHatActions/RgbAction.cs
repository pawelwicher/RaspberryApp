using Emmellsoft.IoT.Rpi.SenseHat;
using Windows.UI;

namespace RaspberryApp.Classes.SenseHatActions
{
    public class RgbAction : BaseAction
    {
        public RgbAction(ISenseHat senseHat) : base(senseHat)
        {
        }

        protected override void Execute()
        {
            byte r = 255;
            byte g = 0;
            byte b = 0;

            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                if (r == 255 && g < 255 && b == 0)
                {
                    g += 1;
                }

                if (g == 255 && r > 0 && b == 0)
                {
                    r -= 1;
                }

                if (g == 255 && b < 255 && r == 0)
                {
                    b += 1;
                }

                if (b == 255 && g > 0 && r == 0)
                {
                    g -= 1;
                }

                if (b == 255 && r < 255 && g == 0)
                {
                    r += 1;
                }

                if (r == 255 && b > 0 && g == 0)
                {
                    b -= 1;
                }

                senseHat.Display.Fill(Color.FromArgb(255, r, g, b));
                senseHat.Display.Update();
                waitEvent.Wait(2);
            }
        }
    }
}