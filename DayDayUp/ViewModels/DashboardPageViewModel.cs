using DayDayUp.Helpers;
using DayDayUp.Models;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Drawing;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Themes;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Nito.AsyncEx;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DayDayUp.ViewModels
{
    public class DashboardPageViewModel : ObservableRecipient
    {
        public ISeries[] LineChartHistory { get; set; }

        public IEnumerable<ICartesianAxis> XAxes { get; set; }

        public IAsyncRelayCommand LoadTaskCommand { get; }

        public int DoingTaskCount { get; set; }

        public int FinishedTaskCount { get; set; }

        //public int TotalCount { get; set; }

        public double DoingTaskBias
        {
            get => doingTaskBias;
            set => SetProperty(ref doingTaskBias, value);
        }

        public double FinishedTaskBias
        {
            get => finishedTaskBias;
            set => SetProperty(ref finishedTaskBias, value);
        }

        public double TotalBias
        {
            get => totalBias;
            set => SetProperty(ref totalBias, value);
        }

        public ObservableCollection<DoingStatics> Statics = new ObservableCollection<DoingStatics>();

        public DashboardPageViewModel(TodoManagementHelper TodoManager)
        {
            todoManager = TodoManager;

            LoadTaskCommand = new AsyncRelayCommand(LoadTaskAsync);

            FinishedTaskCount = todoManager.FinishedTodos.Count;
            DoingTaskCount = todoManager.UnfinishedTodos.Count;

            initCharts();
        }

        private async Task LoadTaskAsync()
        {

            using (await LoadingLock.LockAsync())
            {
                try
                {

                    foreach (Todo item in todoManager.FinishedTodos)
                    {
                        if (item.ExpectedDurationMins != 0)  // scheduled
                        {
                            todoManager.CalDurationAndProgress(item);

                            FinishedTaskBias += updateBias(item);
                        }

                        updateHistory(item);
                    }

                    foreach (Todo item in todoManager.UnfinishedTodos)
                    {
                        if (item.ExpectedDurationMins != 0)
                        {
                            todoManager.CalDurationAndProgress(item);

                            DoingTaskBias += updateBias(item);

                            //TODO: modify
                            if (item.Progress <= 50)
                            {
                                if (!Statics.Contains(progress1))
                                {
                                    Statics.Add(progress1);
                                }
                                progress1.Count += 1;
                            }
                            else if (item.Progress <= 100)
                            {
                                if (!Statics.Contains(progress2))
                                {
                                    Statics.Add(progress2);
                                }
                                progress2.Count += 1;
                            }
                            else
                            {
                                if (!Statics.Contains(progress3))
                                {
                                    Statics.Add(progress3);
                                }
                                progress3.Count += 1;
                            }
                        }
                        else
                        {
                            if (!Statics.Contains(progress0))
                            {
                                Statics.Add(progress0);
                            }
                            progress0.Count += 1;
                        }
                    }
                    TotalBias = DoingTaskBias + FinishedTaskBias;
                    updateStatics();
                }
                catch
                {
                    // Whoops!
                }
            }
        }

        private void initCharts()
        {
            LineChartHistory = new ISeries[] {
                new LineSeries<ObservableValue> {
                    Name = "Finished Tasks",
                    Values = historyCount,
                    Fill= null,
                    Stroke = new SolidColorPaint(new SKColor(color.R, color.G, color.B)){StrokeThickness = 5},
                    },

                new LineSeries<ObservableValue>
                {
                    Name = "Estimated Bias",
                    Values= historyBias,
                    Fill=null,
                }
             };

            xLabel.Add("Today");
            xLabel.Add("Yesterday");
            for (int i = 2; i < 7; i++)
            {
                xLabel.Add(DateTime.Now.AddDays(-i).ToShortDateString());
            }

            XAxes = new Axis[]
            {
                new Axis{
                    Labels = xLabel
                }
            };
        }

        private void updateStatics()
        {
            foreach (var item in Statics)
            {
                var ratio = (decimal)item.Count / DoingTaskCount;
                item.Length = Convert.ToInt32(ratio * 100);
                item.Ratio = (double)ratio;
            }
        }

        private void updateHistory(Todo item)
        {
            var diffDays = DateTime.Now.Subtract(item.TimeStamps.Last()).Days;
            if (diffDays < 7)
            {
                historyCount[diffDays].Value++;
            }
        }

        private double updateBias(Todo task)
        {
            return (task.ExpectedDurationMins - task.DurationMins) / task.ExpectedDurationMins;
        }

        private double doingTaskBias;

        private double finishedTaskBias;

        private double totalBias;

        private List<string> xLabel = new List<string>();

        private LvcColor color = ColorPalletes.FluentDesign[0];

        private readonly TodoManagementHelper todoManager;

        private ObservableValue[] historyCount { get; set; } = new ObservableValue[]
       {
            new ObservableValue(0),
            new ObservableValue(0),
            new ObservableValue(0),
            new ObservableValue(0),
            new ObservableValue(0),
            new ObservableValue(0),
            new ObservableValue(0)
       };

        private ObservableValue[] historyBias { get; set; } = new ObservableValue[]
       {
            new ObservableValue(0),
            new ObservableValue(0),
            new ObservableValue(0),
            new ObservableValue(0),
            new ObservableValue(0),
            new ObservableValue(0),
            new ObservableValue(0)
       };

        private readonly AsyncLock LoadingLock = new AsyncLock();

        private DoingStatics progress0 = new DoingStatics { Name = "Not started", Ratio = 0, Length = 0, Count = 0 };
        private DoingStatics progress1 = new DoingStatics { Name = "0%~50%", Ratio = 0, Length = 0, Count = 0 };
        private DoingStatics progress2 = new DoingStatics { Name = "50%~100%", Ratio = 0, Length = 0, Count = 0 };
        private DoingStatics progress3 = new DoingStatics { Name = ">100%", Ratio = 0, Length = 0, Count = 0 };
    }

    public class DoingStatics
    {
        public string Name { get; set; }
        public double Ratio { get; set; }
        public int Length { get; set; }
        public int Count { get; set; }
    }

}
