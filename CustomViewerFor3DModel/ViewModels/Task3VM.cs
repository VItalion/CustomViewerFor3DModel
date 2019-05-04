using CustomViewerFor3DModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace CustomViewerFor3DModel.ViewModels
{
    class Task3VM : BasePageVM
    {
        private ICommand clearCommand;
        private ICommand loadCommand;

        public ICommand LoadCommand
        {
            get { return loadCommand; }
            set { SetProperty(ref loadCommand, value); }
        }
        public ICommand ClearCommand
        {
            get { return clearCommand; }
            set { SetProperty(ref clearCommand, value); }
        }

        public Task3VM()
        {
            LoadCommand = new RelayCommand(LoadTask, CanLoadTask);
            ClearCommand = new RelayCommand(ClearTask, CanClearTask);
        }

        private async void LoadTask()
        {
            await LoadModel3D();
        }
        private bool CanLoadTask()
        {
            return !IsLoading;
        }
        private void ClearTask()
        {
            Model = null;
        }
        private bool CanClearTask()
        {
            return Model != null;
        }

        private async Task LoadModel3DFromFile(string path)
        {
            var model3dHelper = new Helpers.Model3DHelper();
            Mouse.SetCursor(Cursors.Wait);

            Model = await model3dHelper.GetModel3DAsync(path);

            Mouse.SetCursor(Cursors.Arrow);
            CommandManager.InvalidateRequerySuggested();
        }

        private async Task<bool> LoadModel3D()
        {

            var helper = new Helpers.DialogHelper();
            var result = helper.ShowDialog().Value;
            if (result)
            {
                Model = null;
                IsLoading = true;

                var path = helper.GetFilePath();
                await LoadModel3DFromFile(path);

                IsLoading = false;
            }

            return result;
        }

    }
}
