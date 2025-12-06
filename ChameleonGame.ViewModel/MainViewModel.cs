using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChameleonGame.Model;

namespace ChameleonGame.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region private fields

        private readonly GameModel _model;
        private readonly IFileService _fileService;
        private readonly IMessageService _messageService;
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

        public event EventHandler<int>? NewGame;
        public event EventHandler<string>? SaveGame;
        public event EventHandler<string>? LoadGame;
        public event EventHandler<(int r, int c)>? CellClicked;

        #endregion

        public MainViewModel(GameModel model, IFileService fileService, IMessageService messageService)
        {
            _model = model;
            _fileService = fileService;
            _messageService = messageService;

            _model.BoardChanged += RenderBoard;
            _model.CurrentPlayerChanged += CurrentPlayerChanged;
            _model.GameOver += OnGameOver;
            _model.ErrorOccurred += OnErrorOccurred;

            NewGameCommand = new DelegateCommand(param => {
                if (param is int size)
                    NewGame?.Invoke(this, size);
            });
            SaveGameCommand = new DelegateCommand(param => {
                if (param is string)    
                    SaveGame?.Invoke(this, (string)param);
            }, _ => !_isGameOver);
            LoadGameCommand = new DelegateCommand(param => {
                if (param is string)
                    LoadGame?.Invoke(this, (string)param);
            }, _ => !_isGameOver);
            CellClickedCommand = new DelegateCommand(param => { 
                if (param is (int r, int c))
                    CellClicked?.Invoke(this, (r, c));
            }, _ => !_isGameOver);

            NewGame += OnNewGame;
            SaveGame += OnSaveGame;
            LoadGame += OnLoadGame;
            CellClicked += OnCellClicked;
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
                cellVM.CellImagePath = cell.Color.ToString();
                cellVM.PieceImagePath = cell.Piece?.Owner.ToString() ?? null;
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

        private void OnNewGame(object? sender, int e)
        {
            try
            {
                GenerateBoard();
                _model.NewGame(e);
                BoardSize = e;
            }
            catch (Exception ex)
            {

                OnErrorOccurred(sender, ex.Message);
            }

        }

        private void OnSaveGame(object? sender, string e)
        {
            throw new NotImplementedException();
        }

        private void OnLoadGame(object? sender, string e)
        {
            throw new NotImplementedException();
        }
        private void OnCellClicked(object? sender, (int r, int c) e)
        {
            _model.CellClicked(e.r, e.c);
        }

        #endregion
    }
}
