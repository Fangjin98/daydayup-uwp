using DayDayUp.Helpers;
using DayDayUp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public static readonly int SelectionTimes =1000;

        public DurationPredictionPage(TodoManagementHelper TodoManager, int duration)
        {
            InitializeComponent();
            baseDuration = duration;
            todoManager = TodoManager;
        }

        private void PredictionList_Loaded(object sender, RoutedEventArgs e)
        {
            List<Todo> history = todoManager.FinishedTodos;
            List<predictionItems> montoCarloSelection = new();

            Random random =new Random();

            for ( int i = 0; i < SelectionTimes; i++)
            {
                int index = random.Next(history.Count);
                int tmpDuration = (int)(baseDuration + baseDuration * history[index].Bias);

                if(montoCarloSelection.Count(t => t.DurationMins==tmpDuration) ==0)
                {
                    montoCarloSelection.Add(
                        new predictionItems { DurationMins = tmpDuration, Count = 1 });
                }
                else
                {
                    montoCarloSelection.Find(t => t.DurationMins == tmpDuration).Count += 1;
                }
            }

            foreach (var item in montoCarloSelection.OrderByDescending(x => x.Probability).ToList())
            {
                predictiedDurations.Add(item);
            }
        }

        private readonly TodoManagementHelper todoManager;

        private ObservableCollection<predictionItems> predictiedDurations { get; set; } = new();

        private int baseDuration;
    }
    public class predictionItems
    {
        public int DurationMins { get; set; }
        public int Count { get; set; }
        public double Probability {
            get => (double) Math.Round(Convert.ToDecimal(Count) /
                        Convert.ToDecimal(DurationPredictionPage.SelectionTimes),2);
        }
    }
}
