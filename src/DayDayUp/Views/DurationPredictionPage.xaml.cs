using DayDayUp.Helpers;
using DayDayUp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DayDayUp.Views
{
    public sealed partial class DurationPredictionPage : Page
    {
        public static readonly int SelectionTimes =1000;

        public DurationPredictionPage(TodoManagementHelper TodoManager, int duration)
        {
            InitializeComponent();
            baseDuration = duration;
            historyIn7Days = TodoManager.FinishedTodos.FindAll(t => DateTime.Now.Subtract((DateTime) t.FinishDate).TotalDays <= 15);
        }

        private void PredictionList_Loaded(object sender, RoutedEventArgs e)
        {
            List<PredictionItem> montoCarloSelection = new();

            Random random =new Random();

            for ( int i = 0; i < SelectionTimes; i++)
            {
                int index = random.Next(historyIn7Days.Count);
                int tmpDuration;
                double tmpBias;
                try
                {
                    tmpBias = historyIn7Days[index].Bias;
                    tmpDuration = (int)(baseDuration * (1 + tmpBias));
                }
                catch (ArgumentOutOfRangeException)
                {
                    predictiedDurations.Clear();
                    return;
                }

                if(montoCarloSelection.Count(t => t.DurationMins==tmpDuration) ==0)
                {
                    montoCarloSelection.Add(
                        new PredictionItem { 
                            DurationMins = tmpDuration, 
                            Bias= tmpBias, 
                            Count = 1 });
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

        private List<Todo> historyIn7Days { get; set; } = new();

        private ObservableCollection<PredictionItem> predictiedDurations { get; set; } = new();

        private int baseDuration;
    }
    public class PredictionItem
    {
        public int DurationMins { get; set; }
        public double Bias { get ; set; }
        public int Count { get; set; }
        public double Probability {
            get => (double) Math.Round(Convert.ToDecimal(Count) /
                        Convert.ToDecimal(DurationPredictionPage.SelectionTimes),2);
        }
    }
}
