using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChameleonGame.ViewModel
{
    public class ChameleonMessageService : IMessageService
    {
        public void ShowMessage(string message, string caption)
        {
            // Implement logic to show a message to the user, e.g., using a MessageBox
            Console.WriteLine($"[MESSAGE] {caption}: {message}");
        }
        public void ShowError(string message, string caption)
        {
            // Implement logic to show an error message to the user, e.g., using a MessageBox
            Console.WriteLine($"[ERROR] {caption}: {message}");
        }
    }
}
