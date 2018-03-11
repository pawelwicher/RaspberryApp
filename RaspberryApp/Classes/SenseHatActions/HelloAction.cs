using Emmellsoft.IoT.Rpi.SenseHat;

namespace RaspberryApp.Classes.SenseHatActions
{
    public class HelloAction : BaseAction
    {
        public HelloAction(ISenseHat senseHat) : base(senseHat)
        {
        }

        protected override void Execute()
        {
            var text = "HELLO ";
            var index = 0;

            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                ScreenHelper.DisplayLetter(senseHat, text[index]);
                index = index == text.Length - 1 ? 0 : index + 1;
                waitEvent.Wait(500);
            }
        }
    }
}