using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO.Ports;
using System.Text;
using System.Threading;
using ReactiveUI;
using System.Linq;

namespace AvaloniaSerialManager.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Avalonia!";

        private Timer _stateTimer;

        private long _baudRate;

        public long BaudRate
        {
            get { return _baudRate; }
            set { this.RaiseAndSetIfChanged(ref _baudRate, value); }
        }


        private ObservableCollection<long> _baudRates;
        public ObservableCollection<long> BaudRates
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
        private int _databits;
        public int Databits
        {
            get { return _databits; }
            set { this.RaiseAndSetIfChanged(ref _databits, value); }
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


        private SerialPort _serialPort;

        public MainWindowViewModel()
        {
            _serialPort = new SerialPort();

            _stateTimer = new Timer(UpdateAvailableSerialPorts, null, Timeout.Infinite, 5000);

            _baudRates = new ObservableCollection<long>();
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

            var parityList = Enum.GetValues(typeof(Parity)).Cast<Parity>();
            _parities = new ObservableCollection<Parity>(parityList);

            _databits = 8;

            var stopbits = Enum.GetValues(typeof(StopBits)).Cast<StopBits>();
            _stopBits = new ObservableCollection<StopBits>(stopbits);
            //// Allow the user to set the appropriate properties.
            //_serialPort.PortName = SetPortName(_serialPort.PortName);//OK
            //_serialPort.BaudRate = SetPortBaudRate(_serialPort.BaudRate);//OK
            //_serialPort.Parity = SetPortParity(_serialPort.Parity);//OK
            //_serialPort.DataBits = SetPortDataBits(_serialPort.DataBits);OK
            //_serialPort.StopBits = SetPortStopBits(_serialPort.StopBits);
            //_serialPort.Handshake = SetPortHandshake(_serialPort.Handshake);

            //// Set the read/write timeouts
            //_serialPort.ReadTimeout = 500;
            //_serialPort.WriteTimeout = 500;

            //_serialPort.Open();
            //_continue = true;
            //readThread.Start();


        }

        internal void DisposeInternal()
        {
            if (_stateTimer != null)
                _stateTimer.Dispose();
        }

        public void UpdateAvailableSerialPorts(Object state)
        {

            var serialPorts = new List<string>();
            Debug.WriteLine("[UpdateAvailableSerialPorts]Called");

            foreach (string s in SerialPort.GetPortNames())
            {
                Debug.WriteLine($"{s}");
                if (!string.IsNullOrEmpty(s))
                {
                    //availableSerialPortsNames = string.Join(" ", availableSerialPortsNames, s);
                    serialPorts.Add(s);
                }
            }

            SerialPortNames = new ObservableCollection<string>(serialPorts);
        }

        public void GetSerialPortsCommand()
        {
            //foreach (string s in SerialPort.GetPortNames())
            //{
            //    Console.WriteLine("   {0}", s);
            //}

            Debug.WriteLine("[GetSerialPorts]Called");


            _stateTimer.Change(0, 5000);

            //Content = new AddItemViewModel();
        }

    }
    //https://stackoverflow.com/questions/972307/how-to-loop-through-all-enum-values-in-c
    public static class EnumUtil
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
