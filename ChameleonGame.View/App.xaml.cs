using System.Configuration;
using System.Data;
using System.Windows;
using ChameleonGame.Persistance;
using ChameleonGame.Model;
using ChameleonGame.ViewModel;

namespace ChameleonGame.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IChameleonDataAccess _dataAccess = null!;
        private GameModel _model = null!;
        private MainViewModel _mainViewModel = null!;
        private MainWindow _mainWindow = null!;

        public App()
        {
            Startup += OnStartup;
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            _dataAccess = new ChameleonTxtDataAccess();

            _model = new GameModel(_dataAccess);

            IFileService fileService = new ChameleonFileService();
            IMessageService messageService = new ChameleonMessageService();
            _mainViewModel = new MainViewModel(_model, fileService, messageService);

            _mainWindow = new MainWindow
            {
                DataContext = _mainViewModel
            };
            _mainWindow.Show();
        }
    }

}
