using Emmellsoft.IoT.Rpi.SenseHat;
using System.Threading;
using System.Threading.Tasks;

namespace RaspberryApp.Classes.SenseHatActions
{
    public abstract class BaseAction
    {
        protected ManualResetEventSlim waitEvent;
        protected CancellationTokenSource cancellationTokenSource;
        protected ISenseHat senseHat;

        public BaseAction(ISenseHat senseHat)
        {
            this.waitEvent = new ManualResetEventSlim(false);
            this.cancellationTokenSource = new CancellationTokenSource();
            this.senseHat = senseHat;
        }

        protected abstract void Execute();

        public void Start()
        {
            cancellationTokenSource = new CancellationTokenSource();
            Task.Run(() => Execute(), cancellationTokenSource.Token);
        }

        public void Cancel()
        {
            cancellationTokenSource.Cancel();
        }
    }
}