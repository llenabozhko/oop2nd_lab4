using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Lab4.Class;
using Lab4.DTO;
using Lab4.Interface;
using Lab4.ViewModel;
using Lab4.Forms;
using Lab4.Service;

namespace Lab4
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private MainViewModel vm;
        CommandFactory commandFactory;
        public MainWindow()
        {
            InitializeComponent();
            CommandFactory factory = new CommandFactory((exec, сanc) => new RelayCommand(exec));
            var navigationService = new WPFNavigationService();

            vm = new MainViewModel(factory, navigationService);
            vm.ShowMessage = message => MessageBox.Show(message);
            DataContext = vm;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            vm.SaveConcertsToFile();
        }


    }
}
