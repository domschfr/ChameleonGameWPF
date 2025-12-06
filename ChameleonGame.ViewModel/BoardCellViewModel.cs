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
        private string _cellImagePath = "";
        private string? _pieceImagePath = null;

        public int Row { get; }
        public int Col { get; }

        public string CellImagePath
        {
            get => _cellImagePath;
            set
            {
                if (_cellImagePath != value)
                {
                    _cellImagePath = TransformCellPath(value);
                    OnPropertyChanged();
                }
            }
        }

        public string? PieceImagePath
        {
            get => _pieceImagePath;
            set
            {
                if (_pieceImagePath != value)
                {
                    _pieceImagePath = TransformPiecePath(value);
                    OnPropertyChanged();
                }
            }
        }

        public DelegateCommand CellClickCommand { get; set; }

        public BoardCellViewModel(int row, int col, DelegateCommand cellClickCommand)
        {
            Row = row;
            Col = col;
            CellClickCommand = cellClickCommand;
        }

        private string TransformCellPath(string color)
        {
            return $"cell_{color}";
        }

        private string? TransformPiecePath(string? color)
        {
            return color != null ? $"chameleon_{color}" : null;
        }
    }
}
