using Emmellsoft.IoT.Rpi.SenseHat;
using RaspberryApp.Classes.SenseHatActions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
                    //new OneColorAction(senseHat, Colors.White),
                    //new OneColorAction(senseHat, Colors.DarkRed),
                    //new OneColorAction(senseHat, Colors.DarkGreen),
                    //new OneColorAction(senseHat, Colors.DarkBlue),
                    //new RgbAction(senseHat),
                    //new HelloAction(senseHat),
                    //new ClockAction(senseHat),
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