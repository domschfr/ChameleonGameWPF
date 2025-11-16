using ChameleonGame.Persistance;

namespace ChameleonGame.Model
{
    public enum Player
    {
        Red,
        Green
    }

    public class GameModel
    {
        #region public properties
        public ChameleonBoard? Board { get; private set; }
        public IChameleonDataAccess? DataAccess { get; set; }
        public Player CurrentPlayer { get; private set; }
        public Player? Winner { get; private set; } = null;
        #endregion

        private bool isGameOver = false;
        private (int r, int c)? _selectedCell = null;

        #region events
        public event EventHandler? BoardChanged;
        public event EventHandler? CurrentPlayerChanged;
        public event EventHandler<Player?>? GameOver;
        public event EventHandler<string>? ErrorOccurred;
        #endregion

        #region public methods
        public void NewGame(int size)
        {
            Board = new ChameleonBoard(size);
            Board.InitializePieces();
            CurrentPlayer = Player.Red;

            OnBoardChange();
            OnCurrentPlayerChanged();
        }

        public void LoadGame(string path)
        {
            ChameleonBoardDTO loaded = DataAccess!.LoadGame(path, out PlayerDTO currentPlayer);

            List<Piece> loadedPieces = new();
            foreach (PieceDTO pieceDTO in loaded.Pieces)
            {
                Player owner = pieceDTO.Owner == PlayerDTO.Red ? Player.Red : Player.Green;
                Piece piece = new(owner, pieceDTO.Row, pieceDTO.Col, pieceDTO.ColorChangeDelay);
                loadedPieces.Add(piece);
            }

            Board = new ChameleonBoard(loaded.Size, loadedPieces);

            CurrentPlayer = currentPlayer == PlayerDTO.Red ? Player.Red : Player.Green;

            OnBoardChange();
            OnCurrentPlayerChanged();
        }

        public void SaveGame(string path)
        {
            List<PieceDTO> piecesDTO = new();
            for (int r = 0; r < Board!.Size; r++)
            {
                for (int c = 0; c < Board.Size; c++)
                {
                    Cell cell = Board[r, c];
                    if (cell.Piece != null)
                    {
                        PlayerDTO ownerDTO = cell.Piece.Owner == Player.Red ? PlayerDTO.Red : PlayerDTO.Green;
                        PieceDTO pieceDTO = new()
                        {
                            Owner = ownerDTO,
                            Row = cell.Row,
                            Col = cell.Col,
                            ColorChangeDelay = cell.Piece.ColorChangeDelay
                        };
                        piecesDTO.Add(pieceDTO);
                    }
                }
            }

            ChameleonBoardDTO boardDTO = new()
            {
                Size = Board!.Size,
                Pieces = piecesDTO
            };

            PlayerDTO currentPlayerDTO = CurrentPlayer == Player.Red ? PlayerDTO.Red : PlayerDTO.Green;

            DataAccess!.SaveGame(path, boardDTO, currentPlayerDTO);
        }

        public void EndTurn()
        {
            Board!.PerformColorChange();
            CurrentPlayer = CurrentPlayer == Player.Red ? Player.Green : Player.Red;

            isGameOver = IsGameOver();

            OnCurrentPlayerChanged();
        }

        public bool IsGameOver()
        {
            bool redHasPieces = Board!.PlayerHasPieces(Player.Red);
            bool greenHasPieces = Board!.PlayerHasPieces(Player.Green);

            if (redHasPieces && !greenHasPieces && !isGameOver)
            {
                Winner = Player.Red;
                isGameOver = true;
                OnGameOver(Player.Red);
                return true;
            }

            if (greenHasPieces && !redHasPieces && !isGameOver)
            {
                Winner = Player.Green;
                isGameOver = true;
                OnGameOver(Player.Green);
                return true;
            }

            return false;
        }
        #endregion

        #region event invokers
        private void OnBoardChange()
        {
            BoardChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnCurrentPlayerChanged()
        {
            CurrentPlayerChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnGameOver(Player? winner)
        {
            GameOver?.Invoke(this, winner);
        }

        private void OnErrorOccurred(string message)
        {
            ErrorOccurred?.Invoke(this, message);
        }

        public void OnCellClicked(int r, int c)
        {
            if (_selectedCell == null)
            {
                Cell cell = Board![r, c];
                if (cell.Piece == null)
                {
                    OnErrorOccurred("Please select a cell that contains your piece.");
                    return;
                }

                if (cell.Piece.Owner != CurrentPlayer)
                {
                    OnErrorOccurred("This is not your piece. Please select yours!");
                    return;
                }

                _selectedCell = (r, c);
                OnBoardChange();
            }
            else if (_selectedCell.Value.r == r && _selectedCell.Value.c == c)
            {
                _selectedCell = null;
                OnBoardChange();
            }
            else
            {
                Cell from = Board![_selectedCell.Value.r, _selectedCell.Value.c];
                Cell to = Board![r, c];
                try
                {
                    bool moved = Board!.TryMovePiece(from, to, CurrentPlayer);
                    if (!moved)
                    {
                        bool jumped = Board!.TryJump(from, to, CurrentPlayer);
                        if (!jumped)
                        {
                            OnErrorOccurred("Invalid move or jump!");
                            return;
                        }
                    }
                    _selectedCell = null;
                    EndTurn();
                    OnBoardChange();
                }
                catch (Exception ex)
                {
                    OnErrorOccurred(ex.Message);
                    _selectedCell = null;
                    return;
                }
            }
        }
        #endregion
    }
}
