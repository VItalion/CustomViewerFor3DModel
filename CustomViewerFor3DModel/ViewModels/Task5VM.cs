using CustomViewerFor3DModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace CustomViewerFor3DModel.ViewModels
{
    internal class Task5VM : BasePageVM
    {
        private Storyboard animation;
        private ICommand startAnimationCommand;
        private ICommand stopAnimationCommand;
        private bool isEnabledAnimation;
        private double minZ;
        private double maxZ;
        private bool isAnimateModelInOtherPages;

        public Storyboard Animation
        {
            get { return animation; }
            set { SetProperty(ref animation, value); }
        }

        public double MinZ
        {
            get { return minZ; }
            set { SetProperty(ref minZ, value); }
        }

        public double MaxZ
        {
            get { return maxZ; }
            set { SetProperty(ref maxZ, value); }
        }

        public ICommand StartAnimationCommand
        {
            get { return startAnimationCommand; }
            set { SetProperty(ref startAnimationCommand, value); }
        }
        public ICommand StopAnimationCommand
        {
            get { return stopAnimationCommand; }
            set { SetProperty(ref stopAnimationCommand, value); }
        }

        public bool IsEnabledAnimation
        {
            get { return isEnabledAnimation; }
            set { SetProperty(ref isEnabledAnimation, value); }
        }
        public bool IsAnimateModelInOtherPages
        {
            get => isAnimateModelInOtherPages;
            set => SetProperty(ref isAnimateModelInOtherPages, value);
        }

        public Task5VM()
        {
            MinZ = -25;
            MaxZ = 25;

            StartAnimationCommand = new RelayCommand(StartAnimation, CanStartAnimation);
            StopAnimationCommand = new RelayCommand(StopAnimation, CanStopAnimation);

            PropertyChanged += OnPropertyChanged;
            Models.Model3D.InstanceChanged += ModelInstanceChanged;
        }

        private void ModelInstanceChanged()
        {
            if (IsEnabledAnimation)
            {
                StopAnimation();
            }
        }

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IsCurrent))
            {
                ManageAnimation();
            }
        }

        private void StartAnimation()
        {
            IsEnabledAnimation = true;
            Animation.Begin();
        }
        private void StopAnimation()
        {
            IsEnabledAnimation = false;
            Animation.Stop();
        }

        private bool CanStartAnimation()
        {
            return !IsEnabledAnimation && Model != null;
        }

        private bool CanStopAnimation()
        {
            return IsEnabledAnimation && Model != null;
        }

        private void ManageAnimation()
        {
            if (IsAnimateModelInOtherPages)
                return;

            if (IsCurrent && IsEnabledAnimation)
            {
                Animation.Begin();
            }
            else if (IsEnabledAnimation && Animation.GetCurrentState() != ClockState.Stopped)
            {
                Animation.Stop();
            }
        }
    }
}
