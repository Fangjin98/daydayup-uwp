using DayDayUp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace DayDayUp.Views
{
    public sealed partial class DurationSettingPage : Page
    {
        public DurationSettingPage(object dataContext)
        {
            this.InitializeComponent();

            this.DataContext = dataContext;
            
            for(int i = 0; i <= 60; i++)
            {
                offsetValues.Add(i.ToString()); 
            }

            for (int i = 0; i < 15; i++)
            {
                dayOffsetValues.Add(i.ToString());
            }
        }

        private void OffsetValue_Changed(object sender, SelectionChangedEventArgs e)
        {
            totalOffset=daysOffset*24*60+hoursOffset*60+minutesOffset;
            if (totalOffset != 0)
            {
                var viewModel = (HomePageViewModel)this.DataContext;
                viewModel.SelectedTask.ExpectedDurationMins = totalOffset;
            }
        }


        private List<string> offsetValues = new List<string>();

        private List<string> dayOffsetValues = new List<string>();

        private int daysOffset=0;

        private int hoursOffset=0;

        private int minutesOffset=0;

        private int totalOffset = 0;
    }
}
