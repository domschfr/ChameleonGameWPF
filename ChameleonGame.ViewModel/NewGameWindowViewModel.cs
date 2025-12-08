using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChameleonGame.ViewModel
{
    public class NewGameWindowViewModel : ViewModelBase
    {
        private int _selectedSize;
        private bool? _dialogResult;

        public int SelectedSize
        {
            get => _selectedSize;
            set
            {
                if (_selectedSize != value)
                {
                    _selectedSize = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool? DialogResult
        {
            get => _dialogResult;
            set
            {
                if (_dialogResult != value)
                {
                    _dialogResult = value;
                    OnPropertyChanged();
                }
            }
        }

        public DelegateCommand ConfirmCommand { get; }
        public DelegateCommand CancelCommand { get; }

        public NewGameWindowViewModel()
        {
            ConfirmCommand = new DelegateCommand(_ =>
            {
                DialogResult = true;
            }, _ => SelectedSize == 3 || SelectedSize == 5 || SelectedSize == 7);
            CancelCommand = new DelegateCommand(_ =>
            {
                DialogResult = false;
            });
        }
    }
}
