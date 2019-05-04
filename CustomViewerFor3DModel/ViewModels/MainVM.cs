using CustomViewerFor3DModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using DomainModel = CustomViewerFor3DModel.Models.Model3D;

namespace CustomViewerFor3DModel.ViewModels
{
    class MainVM : BaseVM
    {
        private BasePageVM currentPage;
        private ReadOnlyCollection<BasePageVM> pages;
        private ICommand selectTaskCommand;
        private ICommand loadAnimarionCommand;

        public ReadOnlyCollection<BasePageVM> Pages
        {
            get
            {
                if (pages == null)
                    CreatePages();

                return pages;
            }
        }
        private Model3D Model
        {
            //get => model;
            //set => SetProperty(ref model, value);
            get => DomainModel.Instance;
        }
        public BasePageVM CurrentPage
        {
            get { return currentPage; }
            set
            {
                SetProperty(ref currentPage, value);
            }
        }

        public ICommand SelectTaskCommand
        {
            get { return selectTaskCommand; }
            set { SetProperty(ref selectTaskCommand, value); }
        }
        public ICommand LoadAnimarionCommand
        {
            get => loadAnimarionCommand;
            set => SetProperty(ref loadAnimarionCommand, value);
        }
        public MainVM()
        {
            SelectTaskCommand = new RelayCommand<BasePageVM>(OnSelectTask);
            LoadAnimarionCommand = new RelayCommand<Storyboard>(SetAnimation);
        }

        private void OnSelectTask(BasePageVM selectedPage)
        {
            if (selectedPage == null)
                return;
            //Вынужденная мера. ItemsControl не реализует поведения выбора.
            UnSelectedAll();

            CurrentPage = selectedPage;
        }

        private void UnSelectedAll()
        {
            foreach (var page in Pages)
                page.IsCurrent = false;
        }

        private void CreatePages()
        {
            var pageCollections = new List<BasePageVM>();

            var task3 = new Task3VM();
            task3.DisplayName = "Task3";
            task3.SelectedChanged += OnSelectTask;

            var task4 = new Task4VM();
            task4.DisplayName = "Task4";
            task4.SelectedChanged += OnSelectTask;

            var task5 = new Task5VM();
            task5.DisplayName = "Task5";
            task5.SelectedChanged += OnSelectTask;

            pageCollections.Add(task3);
            pageCollections.Add(task4);
            pageCollections.Add(task5);

            pages = new ReadOnlyCollection<BasePageVM>(pageCollections);

            SelectTaskCommand.Execute(task3);
            task3.IsCurrent = true;
        }

        private void SetAnimation(Storyboard animation)
        {
            var animVm = Pages.FirstOrDefault(p => p is Task5VM);
            if (animVm == null)
                return;

            (animVm as Task5VM).Animation = animation;
        }
    }
}
