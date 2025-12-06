using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChameleonGame.ViewModel
{
    public class ChameleonFileService : IFileService
    {
        public string GetSavePath(string filename)
        {
            // Implement logic to get the save file path, e.g., using a SaveFileDialog
            // For simplicity, returning a hardcoded path here
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
        }
        public string GetLoadPath(string filename)
        {
            // Implement logic to get the load file path, e.g., using an OpenFileDialog
            // For simplicity, returning a hardcoded path here
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
            return File.Exists(path) ? path : string.Empty;
        }
    }
}
