using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RNNEmailClient
{
    /// <summary>
    /// Contains a sendt folder
    /// </summary>
    class Sendt : Folders
    {
        public Sendt(FolderMenu _folderMenu, MainWindow _mw)
            : base(_folderMenu, _mw)
        {
            folderName = "Sendt";
        }
    }
}
