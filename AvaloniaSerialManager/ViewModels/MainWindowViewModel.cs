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
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Avalonia!";

        public ObservableCollection<string> ReceivedData { get; set; }

        //private ObservableCollection<string> _receivedData;
        //public ObservableCollection<string> ReceivedData
        //{
        //    get { return _receivedData; }
        //    set { this.RaiseAndSetIfChanged(ref _receivedData, value); }
        //}


        private Timer _stateTimer;

        private int _baudRate;

        public int BaudRate
        {
            get { return _baudRate; }
            set { this.RaiseAndSetIfChanged(ref _baudRate, value); }
        }


        private ObservableCollection<int> _baudRates;

        internal void DecreaseDatabits()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<int> BaudRates
        {
            get { return _baudRates; }
            set { this.RaiseAndSetIfChanged(ref _baudRates, value); }
        }

        internal void IncreaseDatabits()
        {
            throw new NotImplementedException();
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
            set {
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




        private SerialPort _serialPort;

        public MainWindowViewModel()
        {
            _serialPort = new SerialPort();

            _stateTimer = new Timer(UpdateAvailableSerialPorts, null, Timeout.Infinite, 5000);

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
            //// Allow the user to set the appropriate properties.
            //_serialPort.PortName = SetPortName(_serialPort.PortName);//OK
            //_serialPort.BaudRate = SetPortBaudRate(_serialPort.BaudRate);//OK
            //_serialPort.Parity = SetPortParity(_serialPort.Parity);//OK
            //_serialPort.DataBits = SetPortDataBits(_serialPort.DataBits);//OK
            //_serialPort.StopBits = SetPortStopBits(_serialPort.StopBits);//OK
            //_serialPort.Handshake = SetPortHandshake(_serialPort.Handshake);

            //// Set the read/write timeouts
            //_serialPort.ReadTimeout = 500;
            //_serialPort.WriteTimeout = 500;

            //_serialPort.Open();
            //_continue = true;
            //readThread.Start();

            ReceivedData = new ObservableCollection<string>();
        }

        internal void DisposeInternal()
        {
            if (_stateTimer != null)
            {
                _stateTimer.Dispose();
                _stateTimer = null;
            }

            if (_serialPort != null && _serialPort.IsOpen)
            {
                _serialPort.Close();
                _serialPort.Dispose();
                _serialPort = null;
            }

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

        public void OpenSerialPortCommand()
        {
            if (string.IsNullOrEmpty(_selectedPortName))
                return;
            
            //Take the BaseStream of serialport for async operations
            _serialPort = new SerialPort(_selectedPortName, _baudRate, _parity, _currentDatabits, _currentStopBits);//_serialPort = new SerialPort(_selectedPortName);
            _serialPort.Handshake = _currentHandshake;
            _serialPort.ReadTimeout = 500;//Make these two values variables
            _serialPort.WriteTimeout = 500;
            
            //Eventually create a task for datareceived
            _serialPort.DataReceived += _serialPort_DataReceived;
            Debug.WriteLine("Principal "+ Thread.CurrentThread.ManagedThreadId);
            _serialPort.Open();

        }

        //Which thread execute this portion of code?
        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Debug.WriteLine("Delegate " + Thread.CurrentThread.ManagedThreadId);
            // Thread.Sleep(10000);
            var line=_serialPort.ReadLine();
            Dispatcher.UIThread.Post(() => ReceivedData.Insert(0,line));   
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
