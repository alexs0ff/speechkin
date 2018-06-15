﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SpeechkinApp.Settings;
using Unity;

namespace SpeechkinApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var configuration = new ContainerConfiguration();

            var container = configuration.GetContainer();

            var settings = container.Resolve<SettingsProxy>();
            settings.Load();

            MainWindow = container.Resolve<MainWindow>();

            MainWindow?.Show();

        }
    }
}
