using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaSerialManager.ViewModels;
using System.Diagnostics;

namespace AvaloniaSerialManager.Views
{
    public class MainWindow : Window
    {

        public Button GetSerialPortsButton => this.FindControl<Button>("GetSerialPortsButton");
        public Button OpenSerialPortButton => this.FindControl<Button>("OpenSerialPortButton");
        public ButtonSpinner DatabitsSpinner => this.FindControl<ButtonSpinner>("DatabitsSpinner");
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            DatabitsSpinner.Spin += DatabitsSpinner_Spin;



            DataContext = new MainWindowViewModel();

            this.Closing += MainWindow_Closing;
        }

        private void DatabitsSpinner_Spin(object sender, SpinEventArgs e)
        {
            if (e.Direction.Equals(SpinDirection.Decrease))
            {
                ((MainWindowViewModel)DataContext).Databits -= 1;
                return;
            }
            else if (e.Direction.Equals(SpinDirection.Increase))
            {
                ((MainWindowViewModel)DataContext).Databits += 1;
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.DataContext != null)
                ((MainWindowViewModel)DataContext).DisposeInternal();


            this.Closing -= MainWindow_Closing;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
