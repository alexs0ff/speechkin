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
using System.Windows.Shapes;

namespace SpeechkinApp.Settings
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private readonly SettingsWindowController _controller;

        public SettingsWindow(SettingsWindowController controller)
        {
            _controller = controller;
            InitializeComponent();
            _controller.SetWindow(this);
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            _controller.Close();
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            _controller.Save();
            _controller.Close();
        }
    }
}
