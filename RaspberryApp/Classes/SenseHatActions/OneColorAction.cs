using Emmellsoft.IoT.Rpi.SenseHat;
using Windows.UI;

namespace RaspberryApp.Classes.SenseHatActions
{
    public class OneColorAction : BaseAction
    {
        private Color color;

        public OneColorAction(ISenseHat senseHat, Color color) : base(senseHat)
        {
            this.color = color;
        }

        protected override void Execute()
        {
            senseHat.Display.Fill(color);
            senseHat.Display.Update();
        }
    }
}