using Emmellsoft.IoT.Rpi.SenseHat;

namespace RaspberryApp.Classes.SenseHatActions
{
    public class OffAction : BaseAction
    {
        public OffAction(ISenseHat senseHat) : base(senseHat)
        {
        }

        protected override void Execute()
        {
            senseHat.Display.Clear();
            senseHat.Display.Update();
        }
    }
}