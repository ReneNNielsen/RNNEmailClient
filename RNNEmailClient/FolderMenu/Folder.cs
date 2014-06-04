using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RNNEmailClient
{
    /// <summary>
    /// A folder
    /// </summary>
    class Folder : Folders
    {
        /// <summary>
        /// A folder
        /// </summary>
        /// <param name="_folderMenu">the folder mene</param>
        /// <param name="_mw">the main window</param>
        public Folder(FolderMenu _folderMenu, MainWindow _mw)
            : base(_folderMenu, _mw)
        {
            folderName = "Mapper";
        }
    }
}
