using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChameleonGame.ViewModel
{
    public interface IMessageService
    {
        void ShowMessage(string message, string caption);
        void ShowError(string message, string caption);
    }
}
