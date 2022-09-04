#nullable enable

using DayDayUp.Core.Settings;
using DayDayUp.Helpers;
using DayDayUp.Services;
using DayDayUp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Linq;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace DayDayUp
{

    sealed partial class App : Application
    {
     
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Ioc.Default.ConfigureServices(
               new ServiceCollection()
               //Services
               .AddSingleton<ISettingsProvider, SettingsProvider>()
               .AddSingleton<IDataAccess, LiteDbDataAccess>()
               .AddSingleton<TodoManager>()
               .AddSingleton<ThemeSelector>()
               .AddSingleton<MainPageViewModel>()
               .AddSingleton<SettingsPageViewModel>()
               .AddTransient<HomePageViewModel>()
               .AddTransient<ArchivePageViewModel>()
               .BuildServiceProvider());
            
            string? userdefinedLanguage = Ioc.Default.GetRequiredService<ISettingsProvider>().GetSetting(PredefinedSettings.Language);
            LanguageDefinition languageDefinition
                = LanguageManager.Instance.AvailableLanguages.FirstOrDefault(l => string.Equals(l.InternalName, userdefinedLanguage))
                ?? LanguageManager.Instance.AvailableLanguages[0];
            LanguageManager.Instance.SetCurrentCulture(languageDefinition);

            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: 从之前挂起的应用程序加载状态
                }

                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                Window.Current.Activate();
            }

            Ioc.Default.GetRequiredService<ThemeSelector>().SetRequestedTheme();
        }



        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }
    }
}
