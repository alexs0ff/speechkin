using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.WindowsAPICodePack.Dialogs;

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

        private void OpenDocumentPathClick(object sender, RoutedEventArgs e)
        {
            var dlg = new CommonOpenFileDialog();
            dlg.Title = "Select a documents folder";
            dlg.IsFolderPicker = true;
            dlg.IsFolderPicker = true;
            dlg.AddToMostRecentlyUsedList = false;
            dlg.AllowNonFileSystemItems = false;
            dlg.EnsureFileExists = true;
            dlg.EnsurePathExists = true;
            dlg.EnsureReadOnly = false;
            dlg.EnsureValidNames = true;
            dlg.Multiselect = false;
            dlg.ShowPlacesList = true;

            if (Directory.Exists(_controller.Model.DocumentsPath))
            {
                dlg.InitialDirectory = _controller.Model.DocumentsPath;
            }

            if(dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                var folder = dlg.FileName;
                _controller.Model.DocumentsPath = folder;
            }
            Activate();
        }
    }
}
