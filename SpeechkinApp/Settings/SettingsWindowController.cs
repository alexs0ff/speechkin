using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CSCore.CoreAudioAPI;

namespace SpeechkinApp.Settings
{
    public class SettingsWindowController: WindowControllerBase<SettingsWindowModel>
    {
        private readonly SettingsProxy _settingsProxy;

        private readonly IMapper _mapper;

        private readonly AudioDeviceFacade _audioDeviceFacade;

        public SettingsWindowController(SettingsProxy settingsProxy, IMapper mapper, AudioDeviceFacade audioDeviceFacade)
        {
            _settingsProxy = settingsProxy;
            _mapper = mapper;
            _audioDeviceFacade = audioDeviceFacade;
            Model = _mapper.Map<SettingsWindowModel>(_settingsProxy);

            Model.PropertyChanged+=ModelOnPropertyChanged;

            Model.DataFlowItems.Add(new DataFlowItem { Id = (int)DataFlow.Capture, Text = "Recording device" });
            Model.DataFlowItems.Add(new DataFlowItem { Id = (int)DataFlow.Render, Text = "Loopback device" });
            ChangeDevices();

            Model.SampleRateItems.Add(new SampleRateItem{Value = 16000,Name = "16,000 Hz" });
            Model.SampleRateItems.Add(new SampleRateItem{Value = 22000,Name = "22,050 Hz" });
            Model.SampleRateItems.Add(new SampleRateItem{Value = 44100,Name = "44,100 Hz" });

            Model.BitsPerSampleItems.Add(new BitsPerSampleItem{Value = 16,Name = "16"});

            Model.ChannelItems.Add(new ChannelItem{Value = 1,Name = "1"});
            Model.ChannelItems.Add(new ChannelItem{Value = 2,Name = "2"});


        }

        private void ModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            switch (propertyChangedEventArgs.PropertyName)
            {
                case nameof(Model.SelectedDataFlowId):
                    ChangeDevices();
                    break;
            }
        }

        private void ChangeDevices()
        {
            Model.DeviceItems.Clear();

            foreach (var audioDeviceItem in _audioDeviceFacade.GetDevices((DataFlow)Model.SelectedDataFlowId))
            {
                Model.DeviceItems.Add(audioDeviceItem);
            }
        }

        public void Save()
        {
            _mapper.Map(Model, _settingsProxy);
            _settingsProxy.Save();
        }
    }
}
