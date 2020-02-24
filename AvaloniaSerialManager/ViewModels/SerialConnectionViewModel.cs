using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO.Ports;
using System.Text;
using System.Threading;
using ReactiveUI;
using System.Linq;
using Avalonia.Threading;

namespace AvaloniaSerialManager.ViewModels
{
    public class SerialConnectionViewModel:ViewModelBase
    {
        private int _baudRate;
        public int BaudRate
        {
            get { return _baudRate; }
            set { this.RaiseAndSetIfChanged(ref _baudRate, value); }
        }
        private ObservableCollection<int> _baudRates;
        public ObservableCollection<int> BaudRates
        {
            get { return _baudRates; }
            set { this.RaiseAndSetIfChanged(ref _baudRates, value); }
        }
        private Parity _parity;
        public Parity Parity
        {
            get { return _parity; }
            set { this.RaiseAndSetIfChanged(ref _parity, value); }
        }


        private ObservableCollection<Parity> _parities;
        public ObservableCollection<Parity> Parities
        {
            get { return _parities; }
            set { this.RaiseAndSetIfChanged(ref _parities, value); }
        }

        //Il valore dei bit di dati è minore di 5 o maggiore di 8.//the default value is 8!!!
        private int _currentDatabits;
        public int CurrentDatabits
        {
            get { return _currentDatabits; }
            set
            {
                //https://docs.microsoft.com/it-it/dotnet/api/system.io.ports.serialport.databits?view=netframework-4.8&viewFallbackFrom=netcore-3.1
                if (value < 5 || value > 8)
                    return;
                this.RaiseAndSetIfChanged(ref _currentDatabits, value);
            }
        }

        private StopBits _currentStopBits;

        public StopBits CurrentStopBits
        {
            get { return _currentStopBits; }
            set { this.RaiseAndSetIfChanged(ref _currentStopBits, value); }
        }

        private ObservableCollection<StopBits> _stopBits;
        public ObservableCollection<StopBits> StopBits
        {
            get { return _stopBits; }
            set { this.RaiseAndSetIfChanged(ref _stopBits, value); }
        }


        private string _selectedPortName;
        public string SelectedPortName
        {
            get => _selectedPortName;
            set => this.RaiseAndSetIfChanged(ref _selectedPortName, value);
        }

        private ObservableCollection<string> _serialPortNames;

        public ObservableCollection<string> SerialPortNames
        {
            get => _serialPortNames;
            set => this.RaiseAndSetIfChanged(ref _serialPortNames, value);
        }

        private Handshake _currentHandshake;

        public Handshake CurrentHandshake
        {
            get { return _currentHandshake; }
            set { this.RaiseAndSetIfChanged(ref _currentHandshake, value); }
        }

        private ObservableCollection<Handshake> _handshakes;

        public ObservableCollection<Handshake> Handshakes
        {
            get { return _handshakes; }
            set { this.RaiseAndSetIfChanged(ref _handshakes, value); }
        }


        public SerialConnectionViewModel()
        {
            _baudRates = new ObservableCollection<int>();
            _baudRates.Add(300);
            _baudRates.Add(600);
            _baudRates.Add(1200);
            _baudRates.Add(2400);
            _baudRates.Add(4800);
            _baudRates.Add(9600);
            _baudRates.Add(14400);
            _baudRates.Add(19200);
            _baudRates.Add(28800);
            _baudRates.Add(38400);
            _baudRates.Add(57600);
            _baudRates.Add(115200);

            _baudRate = 9600;

            var parityList = Enum.GetValues(typeof(Parity)).Cast<Parity>();
            _parities = new ObservableCollection<Parity>(parityList);
            _parity = Parity.None;
            _currentDatabits = 8;

            var stopbits = Enum.GetValues(typeof(StopBits)).Cast<StopBits>();
            _stopBits = new ObservableCollection<StopBits>(stopbits);
            _currentStopBits = System.IO.Ports.StopBits.One;

            var handshakes = Enum.GetValues(typeof(Handshake)).Cast<Handshake>();
            _handshakes = new ObservableCollection<Handshake>(handshakes);
            _currentHandshake = Handshake.None;
        }

    }
}
