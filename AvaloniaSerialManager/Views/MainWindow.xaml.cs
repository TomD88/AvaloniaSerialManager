using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaSerialManager.ViewModels;

namespace AvaloniaSerialManager.Views
{
    public class MainWindow : Window
    {

        public Button GetSerialPortsButton => this.FindControl<Button>("GetSerialPortsButton");
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

            DataContext = new MainWindowViewModel();

            this.Closing += MainWindow_Closing;
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
