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
        private string _cellImageFilename = "cell_gray.png";
        private string? _pieceImageFilename = null;

        public int Row { get; }
        public int Col { get; }

        public string CellImageFilename
        {
            get => _cellImageFilename;
            set
            {
                if (_cellImageFilename != value)
                {
                    _cellImageFilename = TransformCellPath(value);
                    OnPropertyChanged();
                }
            }
        }

        public string? PieceImageFilename
        {
            get => _pieceImageFilename;
            set
            {
                if (_pieceImageFilename != value)
                {
                    _pieceImageFilename = TransformPiecePath(value);
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
            return $"cell_{color}.png";
        }

        private string? TransformPiecePath(string? color)
        {
            return color != null ? $"chameleon_{color}.png" : null;
        }
    }
}
