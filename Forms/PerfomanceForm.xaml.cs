using System.Windows;
using Lab4.Class;
using Lab4.DTO;
using Lab4.Interface;
using Lab4.ViewModel;
using Lab4.Service;


namespace Lab4.Forms
{

  public partial class PerfomanceForm : Window
  {
    public Performance Result { get; private set; }
    private PerformanceViewModel vm;
    public PerfomanceForm(Performance? performance = null)
    {
      InitializeComponent();
      CommandFactory commandFactory = new CommandFactory((exec, can) => new RelayCommand(exec));
      vm = new PerformanceViewModel(commandFactory, performance);
      var navigationService = new WPFNavigationService();
      vm.SetNavigationService(navigationService);
      vm.OnSuccess = (perf) =>
      {
        Result = perf;
        DialogResult = true;
        Close();
      };
      vm.ShowMessage = (message) => MessageBox.Show(message);
      DataContext = vm;
    }
  }
}
