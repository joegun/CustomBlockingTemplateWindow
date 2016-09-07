using SynerMed.SHARE.Framework.Libraries.EventHandlers;
using SynerMed.SHARE.Framework.Libraries.ViewModels;
using System;
using System.Windows.Controls;

namespace SynerMed.SHARE.Common.ViewModels.InteractionRequests
{
    public class BlockingTemplateWindowViewModel : ViewModelBase
    {
        public BlockingTemplateWindowViewModel()
        {
            RegionName = $@"RegionName_{Guid.NewGuid()}";
        }

        #region Properties

        private UserControl _userControl;

        private readonly WeakEvent<EventHandler> _onBlockingTemplateClosed = new WeakEvent<EventHandler>();

        public event EventHandler OnBlockingTemplateClosed
        {
            add { _onBlockingTemplateClosed.RegisterHandler(value); }
            remove { _onBlockingTemplateClosed.UnregisterHandler(value); }
        }

        private string _windowHeader;
        public string WindowHeader
        {
            get { return _windowHeader; }
            set
            {
                _windowHeader = value;
                OnPropertyChanged(@"WindowHeader");
            }
        }

        private string _regionName;
        public string RegionName
        {
            get { return _regionName; }
            set
            {
                _regionName = value;
                OnPropertyChanged(@"RegionName");
            }
        }

        private bool _isDisplayed;
        public bool IsDisplayed
        {
            get { return _isDisplayed; }
            set
            {
                _isDisplayed = value;
                OnPropertyChanged(@"IsDisplayed");
            }
        }

        #endregion

        #region Methods

        //public virtual void RegisterViewIntoBlockingTemplateWindowViewModel(UserControl userControl)
        //{
        //    RegionManager.RegisterViewWithRegion(RegionName, userControl.GetType());
        //}

        //public virtual void EnableView(string viewTitle)
        //{
        //    WindowHeader = viewTitle;
        //    IsDisplayed = true;
        //}

        public virtual void EnableView(UserControl userControl, string viewTitle)
        {
            IsDisplayed = true;
            WindowHeader = viewTitle;
            var desiredRegion = RegionManager.Regions[RegionName];
            if (desiredRegion == null) return;
            _userControl = userControl;
            if (!desiredRegion.Views.Contains(_userControl)) desiredRegion.Add(userControl);
        }

        public virtual void DisableView()
        {
            IsDisplayed = false;
            var desiredRegion = RegionManager.Regions[RegionName];
            if (desiredRegion == null) return;
            if (desiredRegion.Views.Contains(_userControl)) desiredRegion.Remove(_userControl);
        }

        public void Close()
        {
            _onBlockingTemplateClosed.Invoke(null, null);
        }

        #endregion
    }
}
