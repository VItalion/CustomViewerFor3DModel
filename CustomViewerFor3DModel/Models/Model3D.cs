using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomViewerFor3DModel.Models
{
    internal static class Model3D
    {
        private static System.Windows.Media.Media3D.Model3D instance;

        internal static event Action InstanceChanged;

        public static System.Windows.Media.Media3D.Model3D Instance
        {
            get => instance;
            set
            {
                if (value == instance)
                    return;

                instance = value;
                InstanceChanged?.Invoke();
            }
        }
    }
}
