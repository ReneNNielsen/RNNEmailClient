using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RNNEmailClient
{
    class Trash : Folders
    {
        public Trash(FolderMenu _folderMenu, MainWindow _mw)
            : base(_folderMenu, _mw)
        {
            folderName = "Papirkurv";
            folderImg = "trash-empty.jpg";
        }
    }
}
