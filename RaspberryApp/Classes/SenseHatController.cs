using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Emmellsoft.IoT.Rpi.SenseHat;
using RaspberryApp.Classes.SenseHatActions;
using Windows.UI;

namespace RaspberryApp.Classes
{
    public class SenseHatController
    {
        private ISenseHat senseHat;
        private ManualResetEventSlim waitEvent;
        private List<BaseAction> actions;
        private int actionIndex;
        private bool useHttpRequestHandler;

        public SenseHatController()
        {
            this.waitEvent = new ManualResetEventSlim(false);
            this.useHttpRequestHandler = false;
        }

        public void Start()
        {
            Task.Run(async () =>
            {
                senseHat = await SenseHatFactory.GetSenseHat();
                actions = new List<BaseAction>
                {
                    new OneColorAction(senseHat, Colors.White),
                    new OneColorAction(senseHat, Colors.Red),
                    new OneColorAction(senseHat, Colors.DarkRed),
                    new OneColorAction(senseHat, Colors.Green),
                    new OneColorAction(senseHat, Colors.DarkGreen),
                    new OneColorAction(senseHat, Colors.Yellow),
                    new OneColorAction(senseHat, Colors.DarkOrange),
                    new OneColorAction(senseHat, Colors.Blue),
                    new OneColorAction(senseHat, Colors.DarkBlue),
                    new OneColorAction(senseHat, Colors.Magenta),
                    new OneColorAction(senseHat, Colors.Purple),
                    new OneColorAction(senseHat, Colors.DarkOrchid),
                    new OneColorAction(senseHat, Colors.GhostWhite),
                    new OneColorAction(senseHat, Colors.LavenderBlush),
                    new OneColorAction(senseHat, Colors.Moccasin),
                    new OneColorAction(senseHat, Colors.MintCream),
                    new OneColorAction(senseHat, Colors.RosyBrown),
                    new OneColorAction(senseHat, Colors.Violet),
                    new OneColorAction(senseHat, Colors.Khaki),
                    new RgbAction(senseHat),
                    new HelloAction(senseHat),
                    new ClockAction(senseHat),
                    new DisplayTextAction(senseHat),
                    new OffAction(senseHat)
                };

                actionIndex = 0;

                if (useHttpRequestHandler)
                {
                    new HttpRequestHandler(new HtmlGenerator(), ExecuteNextAction).Start();
                }

                Loop();
            });
        }

        private void Loop()
        {
            senseHat.Display.Clear();
            senseHat.Display.Update();

            while (true)
            {
                if (senseHat.Joystick.Update())
                {
                    if (senseHat.Joystick.EnterKey == KeyState.Pressed)
                    {
                        ExecuteNextAction();
                    }
                }

                waitEvent.Wait(2);
            }
        }

        private void ExecuteNextAction()
        {
            actions.ForEach(x => x.Cancel());
            actions[actionIndex].Start();
            actionIndex = actionIndex == actions.Count - 1 ? 0 : actionIndex + 1;
        }
    }
}