using RaspberryApp.Classes;
using Windows.UI.Xaml.Controls;

namespace RaspberryApp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            new SenseHatController().Start();
        }
    }
}