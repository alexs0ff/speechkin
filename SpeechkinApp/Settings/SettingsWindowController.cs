using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace SpeechkinApp.Settings
{
    public class SettingsWindowController: WindowControllerBase<SettingsWindowModel>
    {
        private readonly SettingsProxy _settingsProxy;

        private readonly IMapper _mapper;

        public SettingsWindowController(SettingsProxy settingsProxy, IMapper mapper)
        {
            _settingsProxy = settingsProxy;
            _mapper = mapper;
            Model = _mapper.Map<SettingsWindowModel>(_settingsProxy);
        }

        public void Save()
        {
            _mapper.Map(Model, _settingsProxy);
            _settingsProxy.Save();
        }
    }
}
