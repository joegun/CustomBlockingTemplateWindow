using System.Windows;
using System;
using Microsoft.Practices.Prism.Regions;
using SynerMed.SHARE.Common.ViewModels.InteractionRequests;

namespace SynerMed.SHARE.Common.Views.InteractionRequests
{
    public partial class BlockingTemplateWindowView
    {
        public BlockingTemplateWindowView()
        {
            InitializeComponent();
            Visibility = Visibility.Hidden;
            Loaded += OnUserControlLoaded;
        }

        #region Fields
        private bool _isParentEnabled = true;
        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty IsDisplayedProperty =
            DependencyProperty.Register(@"IsDisplayed", typeof (bool), typeof (BlockingTemplateWindowView),
                new UIPropertyMetadata(false, IsDisplayedChangedCallback));

        public static readonly DependencyProperty OverlayOnTopOfProperty =
            DependencyProperty.Register(@"OverlayOnTopOf", typeof (UIElement), typeof (BlockingTemplateWindowView),
                new UIPropertyMetadata(null));
        #endregion

        #region Properties
        private BlockingTemplateWindowViewModel viewModel => DataContext as BlockingTemplateWindowViewModel;

        public UIElement OverlayOnTopOf
        {
            get { return (UIElement)GetValue(OverlayOnTopOfProperty); }
            set { SetValue(OverlayOnTopOfProperty, value); }
        }

        public bool IsDisplayed
        {
            get { return (bool)GetValue(IsDisplayedProperty); }
            set { SetValue(IsDisplayedProperty, value); }
        }
        #endregion

        #region Methods

        private void OnUserControlLoaded(object sender, RoutedEventArgs e)
        {
            viewModel.OnBlockingTemplateClosed += OnViewModelBlockingTemplateClosed;
            RegionManager.SetRegionName(BlockingTemplateContentControl, viewModel.RegionName);
            RegionManager.SetRegionManager(BlockingTemplateContentControl, viewModel.RegionManager);
        }

        private void OnViewModelBlockingTemplateClosed(object sender, EventArgs e)
        {
            viewModel.OnBlockingTemplateClosed -= OnViewModelBlockingTemplateClosed;
            RegionManager.SetRegionManager(BlockingTemplateContentControl, null);
        }

        public static void IsDisplayedChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                var dlg = (BlockingTemplateWindowView)d;
                dlg.EnableView();
            }
            else
            {
                var dlg = (BlockingTemplateWindowView)d;
                dlg.DisableView();
            }
        }

        public void EnableView()
        {
            var expressionOverlayParent = GetBindingExpression(OverlayOnTopOfProperty);
            expressionOverlayParent?.UpdateTarget();
            if (OverlayOnTopOf == null) throw new InvalidOperationException("Can't get the UI Element where the window should reside.");
            Visibility = Visibility.Visible;
            _isParentEnabled = OverlayOnTopOf.IsEnabled;
            OverlayOnTopOf.IsEnabled = false;
        }

        private void DisableView()
        {
            Visibility = Visibility.Hidden;
            OverlayOnTopOf.IsEnabled = _isParentEnabled;
        }

        #endregion
    }
}
