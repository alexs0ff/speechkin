using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SpeechkinApp.Settings;
using SpeechkinApp.Speech;

namespace SpeechkinApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// TODO: rewrite to navigation layout
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SpeechkinController _controller;

        public MainWindow(SpeechkinController controller, WindowFabric windowFabric)
        {
            _controller = controller;
            InitializeComponent();

            _controller.SetWindow(this);
        }


        private void ShowOptionsClick(object sender, RoutedEventArgs e)
        {
            _controller.ShowSettings();
        }

        private void StartClick(object sender, RoutedEventArgs e)
        {
            _controller.StartRecognition();
        }

        private void StopClick(object sender, RoutedEventArgs e)
        {
            _controller.Stop();
        }
    }
}
