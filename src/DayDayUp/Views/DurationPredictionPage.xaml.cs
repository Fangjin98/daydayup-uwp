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
    public sealed partial class DurationPredictionPage : Page
    {
        public DurationPredictionPage(int duration)
        {
            this.InitializeComponent();
            this.duration = duration;
            predictiedDurations.Add(
                new predictionItems { DurationMins = 10, Probability = 0.1 });
            predictiedDurations.Add(
               new predictionItems { DurationMins = 11, Probability = 0.3 });
            predictiedDurations.Add(
               new predictionItems { DurationMins = 12, Probability = 0.6 });
        }

        private List<predictionItems> predictiedDurations = new List<predictionItems>();

        private int duration;

    }
    public class predictionItems
    {
        public int DurationMins { get; set; }
        public double Probability { get; set; }
    }
}
