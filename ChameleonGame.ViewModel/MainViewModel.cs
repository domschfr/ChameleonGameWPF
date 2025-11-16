using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChameleonGame.Model;
using ChameleonGame.Persistance;

namespace ChameleonGame.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region private fields

        private readonly GameModel _model;
        private bool _isGameOver = false;
        private string _currentPlayer = string.Empty;
        private int _boardSize;

        #endregion

        #region public properties

        public ObservableCollection<BoardCellViewModel> BoardCells { get; private set; }
        public string CurrentPlayer
        {
            get => _currentPlayer;
            private set
            {
                _currentPlayer = value;
                OnPropertyChanged();
            }
        }
        public int BoardSize
        {
            get => _boardSize;
            private set
            {
                _boardSize = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public DelegateCommand NewGameCommand { get; private set; }
        public DelegateCommand SaveGameCommand { get; private set; }
        public DelegateCommand LoadGameCommand { get; private set; }
        public DelegateCommand CellClickedCommand { get; private set; }

        #endregion

        #region Events

        public event EventHandler? NewGameClicked;
        public event EventHandler? SaveGameClicked;
        public event EventHandler? LoadGameClicked;

        #endregion

        public MainViewModel()
        {
            _model = new GameModel();
            _model.DataAccess = new ChameleonTxtDataAccess();

            _model.BoardChanged += OnBoardChanged;
            _model.CurrentPlayerChanged += OnCurrentPlayerChanged;
            _model.GameOver += OnGameOver;
            _model.ErrorOccurred += OnErrorOccurred;

            NewGameCommand = new DelegateCommand(_ => );
            SaveGameCommand = new DelegateCommand(_ => SaveGame?.Invoke(this, EventArgs.Empty), _ => !_isGameOver);
            LoadGameCommand = new DelegateCommand(_ => LoadGame?.Invoke(this, EventArgs.Empty), _ => !_isGameOver);
            CellClickedCommand = new DelegateCommand(_ => OnCellClicked, _ => !_isGameOver);

        }

        #region Event invokers

        private void OnNewGame()
        {
            NewGameClicked?.Invoke(this, EventArgs.Empty);
        }

        private void 

        
    }
}
