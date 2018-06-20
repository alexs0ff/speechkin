using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CSCore.CoreAudioAPI;


namespace SpeechkinApp.Settings
{
    public class SettingsWindowModel:INotifyPropertyChanged
    {
        private string _bingSpeechAuthUrl;

        private string _bingSpeechKey1;

        private string _bingSpeechKey2;

        private int _selectedDataFlowItem;

        private int _inputDeviceIndex;

        private int _sampleRateValue;

        private int _bitsPerSampleValue;

        private int _channelValue;

        public event PropertyChangedEventHandler PropertyChanged;

        public SettingsWindowModel()
        {
            DataFlowItems = new ObservableCollection<DataFlowItem>();
            DeviceItems = new ObservableCollection<AudioDeviceItem>();
            SampleRateItems = new ObservableCollection<SampleRateItem>();
            BitsPerSampleItems = new ObservableCollection<BitsPerSampleItem>();
            ChannelItems = new ObservableCollection<ChannelItem>();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string AzureSpeechAuthUrl
        {
            get { return _bingSpeechAuthUrl; }
            set
            {
                _bingSpeechAuthUrl = value;
                OnPropertyChanged();
            }
        }

        public string AzureSpeechPrimaryKey
        {
            get { return _bingSpeechKey1; }
            set
            {
                _bingSpeechKey1 = value;
                OnPropertyChanged();
            }
        }

        public string AzureSpeechSecondaryKey
        {
            get { return _bingSpeechKey2; }
            set
            {
                _bingSpeechKey2 = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DataFlowItem> DataFlowItems { get;private set; }

        public int SelectedDataFlowId
        {
            get { return _selectedDataFlowItem; }
            set
            {
                _selectedDataFlowItem = value; 
                OnPropertyChanged();

            }
        }

        public int InputDeviceIndex
        {
            get { return _inputDeviceIndex; }
            set
            {
                _inputDeviceIndex = value; 
                OnPropertyChanged();
            }
        }

        public ObservableCollection<AudioDeviceItem> DeviceItems { get; }

        public ObservableCollection<SampleRateItem> SampleRateItems { get; }

        public int SampleRateValue
        {
            get { return _sampleRateValue; }
            set
            {
                _sampleRateValue = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<BitsPerSampleItem> BitsPerSampleItems { get; }

        public int BitsPerSampleValue
        {
            get { return _bitsPerSampleValue; }
            set
            {
                _bitsPerSampleValue = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ChannelItem> ChannelItems { get;  }

        public int ChannelValue
        {
            get { return _channelValue; }
            set
            {
                _channelValue = value;
                OnPropertyChanged();
            }
        }
    }
}
