using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomViewerFor3DModel.Helpers
{
    internal class DialogHelper
    {
        private OpenFileDialog fileDialog;

        internal DialogHelper()
        {
            InitDialog();
        }

        private void InitDialog()
        {
            fileDialog = new OpenFileDialog();
            fileDialog.Filter = "3D object|*.obj";
            fileDialog.Multiselect = false;
        }

        internal bool? ShowDialog()
        {
            var mainWindow = App.Current.MainWindow;
            return fileDialog.ShowDialog(mainWindow);
        }

        internal string GetFilePath()
        {
            return fileDialog.FileName;
        }
    }
}
