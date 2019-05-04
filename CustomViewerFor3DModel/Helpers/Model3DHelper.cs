using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace CustomViewerFor3DModel.Helpers
{
    internal class Model3DHelper
    {
        internal Model3D GetModel3D(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return null;

            Model3D device = null;
            try
            {
                ModelImporter import = new ModelImporter();

                device = import.Load(path);
            }
            catch (Exception e)
            {
                // Logged exception
            }

            return device;
        }

        internal async Task<Model3D> GetModel3DAsync(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return null;

            Model3D device = null;

            await Task.Run(() =>
            {
                try
                {
                    device = GetModel3D(path);
                    device.Freeze();
                }
                catch (Exception e)
                {
                    // Logged exception
                }
            });

            return device;
        }
    }
}
