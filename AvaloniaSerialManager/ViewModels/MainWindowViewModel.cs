using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Text;
using System.Threading;
using ReactiveUI;

namespace AvaloniaSerialManager.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Avalonia!";
        
        private Timer _stateTimer;

        private string _serialPortsString;
        public string SerialPortsString
        {
            get => _serialPortsString;
            set => this.RaiseAndSetIfChanged(ref _serialPortsString, value);
        }


        private SerialPort _serialPort;

        public MainWindowViewModel()
        {
            _serialPort = new SerialPort();


            _stateTimer = new Timer(UpdateAvailableSerialPorts,null,Timeout.Infinite,5000);
            
            
        }

        internal void DisposeInternal()
        {
            if (_stateTimer != null)
                 _stateTimer.Dispose();
        }

        public void UpdateAvailableSerialPorts(Object state) {

            var availableSerialPortsNames = String.Empty;
            Debug.WriteLine("[UpdateAvailableSerialPorts]Called");

            foreach (string s in SerialPort.GetPortNames())
            {
                Debug.WriteLine($"{s}");
                if (!string.IsNullOrEmpty(s))
                    availableSerialPortsNames=string.Join(" ", availableSerialPortsNames, s);
            }

            SerialPortsString = availableSerialPortsNames;

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
}
