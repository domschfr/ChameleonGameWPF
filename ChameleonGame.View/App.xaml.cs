using System.Configuration;
using System.Data;
using System.Windows;
using ChameleonGame.Persistance;
using ChameleonGame.Model;
using ChameleonGame.ViewModel;
using Microsoft.Win32;

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

            _mainViewModel = new MainViewModel(_model);

            _mainViewModel.NewGame += ViewModel_NewGame;
            _mainViewModel.SaveGame += ViewModel_SaveGame;
            _mainViewModel.LoadGame += ViewModel_LoadGame;
            _mainViewModel.GameOver += ViewModel_GameOver;
            _mainViewModel.ErrorOccurred += ViewModel_ErrorOccurred;


            _mainWindow = new MainWindow
            {
                DataContext = _mainViewModel
            };
            _mainWindow.Show();
        }

        private void ViewModel_NewGame(object? sender, EventArgs e)
        {
            NewGameWindow window = new NewGameWindow();
            NewGameWindowViewModel viewModel = new NewGameWindowViewModel();
            window.DataContext = viewModel;

            if (window.ShowDialog() == true && viewModel.SelectedSize != null)
            {
                _mainViewModel.NewGameCommand.Execute(viewModel.SelectedSize);
            }
        }

        private void ViewModel_SaveGame(object? sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Save Game",
                Filter = "Chameleon Game (*.txt)|*.txt|All Files (*.*)|*.*",
                DefaultExt = "txt",
                AddExtension = true
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    _mainViewModel.SaveGameCommand.Execute(saveFileDialog.FileName);
                }
                catch (Exception ex)
                {

                    ViewModel_ErrorOccurred(this, ex.Message);
                }
            }
        }

        private void ViewModel_LoadGame(object? sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Load Game",
                Filter = "Chameleon Game (*.txt)|*.txt|All Files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    _mainViewModel.LoadGameCommand.Execute(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    ViewModel_ErrorOccurred(this, ex.Message);
                }
            }
        }

        private void ViewModel_GameOver(object? sender, Player e)
        {
            string winner = e == Player.Red ? "Red" : "Green";
            MessageBox.Show($"{winner} player wins!", "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ViewModel_ErrorOccurred(object? sender, string e)
        {
            MessageBox.Show(e, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
