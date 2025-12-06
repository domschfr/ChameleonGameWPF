using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChameleonGame.ViewModel
{
    public interface IFileService
    {
        string GetSavePath(string filename);
        string GetLoadPath(string filename);
    }
}
