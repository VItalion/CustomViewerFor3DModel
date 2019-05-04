using CustomViewerFor3DModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using DomainModel = CustomViewerFor3DModel.Models.Model3D;

namespace CustomViewerFor3DModel.ViewModels
{
    internal abstract class BasePageVM : BaseVM
    {
        private string displayName;
        private bool isCurrent;
        private bool isLoading;
        private ICommand selectCommand;

        public event Action<BasePageVM> SelectedChanged;

        public Model3D Model
        {
            get => DomainModel.Instance;
            set
            {
                DomainModel.Instance = value;
                OnPropertyChanged();
            }
        }
        public string DisplayName
        {
            get => displayName;
            set => SetProperty(ref displayName, value);
        }
        public bool IsCurrent
        {
            get => isCurrent;
            set
            {
                if (value)
                    SelectedChanged?.Invoke(this);

                SetProperty(ref isCurrent, value);
            }
        }
        public bool IsLoading
        {
            get => isLoading;
            set => SetProperty(ref isLoading, value);
        }
        public ICommand SelectCommand
        {
            get => selectCommand;
            set => SetProperty(ref selectCommand, value);
        }

        protected BasePageVM()
        {
            SelectCommand = new RelayCommand(() => IsCurrent = true);
        }
    }
}
