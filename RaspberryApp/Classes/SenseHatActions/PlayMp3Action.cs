using System;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Emmellsoft.IoT.Rpi.SenseHat;

namespace RaspberryApp.Classes.SenseHatActions
{
    public class PlayMp3Action : BaseAction
    {
        public PlayMp3Action(ISenseHat senseHat) : base(senseHat)
        {
        }

        protected override void Execute()
        {
            senseHat.Display.Fill(Colors.White);
            senseHat.Display.Update();

            var mediaElement = new MediaElement();
            mediaElement.AudioCategory = AudioCategory.Media;

            mediaElement.Source = new Uri(@"some.mp3");
            mediaElement.Play();
        }
    }
}