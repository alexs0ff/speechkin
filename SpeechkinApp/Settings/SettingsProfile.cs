using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace SpeechkinApp.Settings
{
    public class SettingsProfile: Profile
    {
        public SettingsProfile()
        {
            CreateMap<SettingsProxy, SettingsWindowModel>();
            CreateMap<SettingsWindowModel, SettingsProxy>();
        }
    }
}
