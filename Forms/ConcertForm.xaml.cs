using System.Windows;
using Lab4.Class;
using Lab4.Interface;
using Lab4.ViewModel;
using Lab4.Service;

namespace Lab4.Forms
{

  public partial class ConcertForm : Window
  {
    public Concert Result { get; private set; }

    private readonly ConcertViewModel vm;
    public ConcertForm(Concert? concert = null)
    {
      InitializeComponent();
      CommandFactory commandFactory = new CommandFactory((exec, can) => new RelayCommand(exec));
      vm = new ConcertViewModel(commandFactory, concert);
      var navigationService = new WPFNavigationService();
      vm.SetNavigationService(navigationService);

      vm.ShowMessage = message => MessageBox.Show(message);
      vm.OnSuccess = (concert) =>
      {
        Result = concert;
        DialogResult = true;
        Close();
      };
      DataContext = vm;
    }
  }
}
