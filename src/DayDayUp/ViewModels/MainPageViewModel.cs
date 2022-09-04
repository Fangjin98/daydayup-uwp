using DayDayUp.Services;
using DayDayUp.Views;
using System.Collections.Generic;

namespace DayDayUp.ViewModels
{
    public class MainPageViewModel
    {
        internal MainPageStrings Strings => LanguageManager.Instance.MainPage;

        internal List<Scenario> Scenarios { get; }

        public MainPageViewModel()
        {
            Scenarios = new()
            {
                new Scenario() { Title = Strings.Home, ClassName = typeof(HomePage).FullName, Icon = "\uE80F" },
                new Scenario() { Title = Strings.Archive, ClassName = typeof(ArchivePage).FullName, Icon = "\uE8B7" }
            };
        }
    }

    internal class Scenario
    {
        public string Title { get; set; }
        public string ClassName { get; set; }
        public string Icon { get; set; }
    }
}
