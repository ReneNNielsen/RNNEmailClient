using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RNNEmailClient
{
    /// <summary>
    /// A Trash folder
    /// </summary>
    class Trash : Folders
    {
        /// <summary>
        /// An Trash folder
        /// </summary>
        /// <param name="_folderMenu">the folder mene</param>
        /// <param name="_mw">the main window</param>
        public Trash(FolderMenu _folderMenu, MainWindow _mw)
            : base(_folderMenu, _mw)
        {
            folderName = "Papirkurv";
            folderImg = "trash-empty.jpg";
        }
    }
}
