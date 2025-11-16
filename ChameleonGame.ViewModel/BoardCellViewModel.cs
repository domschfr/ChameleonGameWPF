using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChameleonGame.ViewModel
{
    public class BoardCellViewModel : ViewModelBase
    {
        private string _cellImagePath;
        private string? _pieceImagePath;

        public int Row { get; }
        public int Col { get; }

        public string CellColorBrush
        {
            get => _cellImagePath;
            set
            {
                if (_cellImagePath != value)
                {
                    _cellImagePath = value;
                    OnPropertyChanged();
                }
            }
        }

        public string? PieceColorBrush
        {
            get => _pieceImagePath;
            set
            {
                if (_pieceImagePath != value)
                {
                    _pieceImagePath = value;
                    OnPropertyChanged();
                }
            }
        }

        public BoardCellViewModel(int row, int col, string cellImagePath, string? pieceImagePath)
        {
            Row = row;
            Col = col;
            _cellImagePath = cellImagePath;
            _pieceImagePath = pieceImagePath;
        }
    }
}
