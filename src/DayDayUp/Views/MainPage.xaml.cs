using DayDayUp.ViewModels;
using DayDayUp.Views;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace DayDayUp
{

    public sealed partial class MainPage : Page
    {
        public MainPageViewModel ViewModel => (MainPageViewModel)DataContext;

        public MainPage()
        {
            this.InitializeComponent();

            DataContext = Ioc.Default.GetRequiredService<MainPageViewModel>();

            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;

            Window.Current.SetTitleBar(AppTitleBar);

            SizeChanged += MainPage_SizeChanged;
        }

        private void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateVisualState();
        }

        private void UpdateVisualState()
        {
            //var view = ApplicationView.GetForCurrentView();
            //bool isCompactOverlayMode = view.ViewMode == ApplicationViewMode.CompactOverlay;

            //if (isCompactOverlayMode)
            //{
            //    VisualStateManager.GoToState(this, CompactOverlayStateName, useTransitions: true);
            //}
            //else
            //{
            //switch ((NavigationViewDisplayMode)ViewModel.NavigationViewDisplayMode)
            //{
            //    case NavigationViewDisplayMode.Minimal:
            //        VisualStateManager.GoToState(this, NavigationViewMinimalStateName, useTransitions: true);
            //        break;

            //    case NavigationViewDisplayMode.Compact:
            //        VisualStateManager.GoToState(this, NavigationViewCompactStateName, useTransitions: true);
            //        break;

            //    case NavigationViewDisplayMode.Expanded:
            //        VisualStateManager.GoToState(this, NavigationViewExpandedStateName, useTransitions: true);
            //        break;
            //}
            //}
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Scenario item in ViewModel.Scenarios)
            {
                NavView.MenuItems.Add(new Microsoft.UI.Xaml.Controls.NavigationViewItem
                {
                    Content = item.Title,
                    Tag = item.ClassName,
                    Icon = new FontIcon() { FontFamily = new("Segoe Fluent Icons"), Glyph = item.Icon }
                });
            }


            NavView.SelectedItem = NavView.MenuItems[0];

            if (ViewModel.Scenarios is not null && ViewModel.Scenarios.Count > 0)
            {
                NavView_Navigate(ViewModel.Scenarios.First().ClassName, new Windows.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo());
            }
        }

        private void NavView_Navigate(string navItemTag, Windows.UI.Xaml.Media.Animation.NavigationTransitionInfo transitionInfo)
        {
            Type page;

            if (navItemTag == nameof(SettingsPage))
            {
                page = typeof(SettingsPage);
            }
            else
            {
                Scenario item = ViewModel.Scenarios.First(p => p.ClassName.Equals(navItemTag));
                page = Type.GetType(item.ClassName);
            }

            var preNavPageType = ContentFrame.CurrentSourcePageType;

            if (page is not null && !Type.Equals(preNavPageType, page))
            {
                ContentFrame.Navigate(page, null, transitionInfo);
            }
        }

        private void NavView_BackRequested(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();
            }
        }

        private void NavView_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            var naViewItemInvoked = (Microsoft.UI.Xaml.Controls.NavigationViewItem)args.InvokedItemContainer;

            if (args.IsSettingsInvoked)
            {
                NavView_Navigate(nameof(SettingsPage), args.RecommendedNavigationTransitionInfo);
            }
            else if (args.InvokedItemContainer is not null)
            {
                var navItemTag = args.InvokedItemContainer.Tag?.ToString();
                if (!string.IsNullOrEmpty(navItemTag))
                {
                    NavView_Navigate(navItemTag, new Windows.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo());
                }
            }
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            NavView.IsBackEnabled = ContentFrame.CanGoBack;

            if (ContentFrame.SourcePageType == typeof(SettingsPage))
            {
                NavView.SelectedItem = (Microsoft.UI.Xaml.Controls.NavigationViewItem)NavView.SettingsItem;
               
            }
            else if (ContentFrame.SourcePageType != null)
            {
                var item = ViewModel.Scenarios.First(p => p.ClassName == e.SourcePageType.FullName);

                NavView.SelectedItem = NavView.MenuItems
                    .OfType<Microsoft.UI.Xaml.Controls.NavigationViewItem>()
                    .First(n => n.Tag.Equals(item.ClassName));
            }

            NavView.Header =
                   ((Microsoft.UI.Xaml.Controls.NavigationViewItem)NavView.SelectedItem)?.Content?.ToString();
        }
    }
}
