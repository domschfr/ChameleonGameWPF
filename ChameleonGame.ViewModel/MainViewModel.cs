using ChameleonGame.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChameleonGame.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region private fields

        private readonly GameModel _model;
        private bool _isGameOver = false;
        private string _currentPlayer = "";
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

        public event EventHandler<int>? NewGame;
        public event EventHandler<string>? SaveGame;
        public event EventHandler<string>? LoadGame;
        public event EventHandler<(int r, int c)>? CellClicked;

        #endregion

        public MainViewModel(GameModel model)
        {
            _model = model;

            _model.BoardChanged += RenderBoard;
            _model.CurrentPlayerChanged += CurrentPlayerChanged;
            _model.GameOver += OnGameOver;
            _model.ErrorOccurred += OnErrorOccurred;

            NewGameCommand = new DelegateCommand(param => {
                if (param is int size)
                {
                    try
                    {
                        GenerateBoard();
                        _model.NewGame(size);
                        BoardSize = size;
                    }
                    catch (Exception ex)
                    {

                        throw new NotImplementedException();
                    }
                }
                    
            });
            SaveGameCommand = new DelegateCommand(param => {
                if (param is string)
                    _model.SaveGame((string)param);
            }, _ => !_isGameOver);
            LoadGameCommand = new DelegateCommand(param => {
                if (param is string)
                    _model.LoadGame((string)param);
            }, _ => !_isGameOver);
            CellClickedCommand = new DelegateCommand(param => { 
                if (param is (int r, int c))
                    _model.CellClicked(r, c);
            }, _ => !_isGameOver);

        }

        #region Event handlers

        private void GenerateBoard()
        {
            BoardCells.Clear();
            for (int r = 0; r < BoardSize; r++)
            {
                for (int c = 0; c < BoardSize; c++)
                {
                    BoardCells.Add(new BoardCellViewModel(r, c, CellClickedCommand));
                }
            }
        }

        private void RenderBoard(object? sender, ChameleonBoard board)
        {
            if (_model == null)
                return;

            foreach (var cellVM in BoardCells) { 
                Cell cell = board.Board[cellVM.Row, cellVM.Col];
                cellVM.CellImageFilename = cell.Color.ToString();
                cellVM.PieceImageFilename = cell.Piece?.Owner.ToString() ?? null;
            }
        }

        private void CurrentPlayerChanged(object? sender, Player player)
        {
            CurrentPlayer = player.ToString();
        }

        private void OnGameOver(object? sender, Player? e)
        {
            throw new NotImplementedException();
        }

        private void OnErrorOccurred(object? sender, string e)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
