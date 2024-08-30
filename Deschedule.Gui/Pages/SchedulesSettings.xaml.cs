using Deschedule.Main.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Deschedule.Gui
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SchedulesSettings : Page
    {
        public static ObservableCollection<Schedule> getLists = new ObservableCollection<Schedule>();

        public Microsoft.UI.Dispatching.DispatcherQueue dispatcher = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();

        public SchedulesSettings()
        {
            this.InitializeComponent();
            ClassListView.ItemsSource = ScheduleMgr.Schedules;
            this.DataContext = ScheduleMgr.Schedules;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            ScheduleMgr.Schedules.Add(new Schedule { Id = ScheduleMgr.Schedules.Count + 1, startHr = StartTime.Time.Hours, startMin = StartTime.Time.Minutes, endHr = EndTime.Time.Hours, endMin = EndTime.Time.Minutes, lessonType = LessonName.Text });
            // ScheduleMgr.PushData(getList);
        }

        private void DateSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

                switch (DateSelection.SelectedItem)
                {
                    case "周一":
                        MainWindow.FilePath = Environment.CurrentDirectory + "\\ScheduleSets\\" + "Monday.json";
                        ScheduleMgr.ReadDataForUI(MainWindow.FilePath, dispatcher);
                        break;

                    case "周二":
                        MainWindow.FilePath = Environment.CurrentDirectory + "\\ScheduleSets\\" + "Tuesday.json";
                        ScheduleMgr.ReadDataForUI(MainWindow.FilePath, dispatcher);
                        break;

                    case "周三":
                        MainWindow.FilePath = Environment.CurrentDirectory + "\\ScheduleSets\\" + "Wednesday.json";
                        ScheduleMgr.ReadDataForUI(MainWindow.FilePath, dispatcher);
                        break;

                    case "周四":
                        MainWindow.FilePath = Environment.CurrentDirectory + "\\ScheduleSets\\" + "Thursday.json";
                        ScheduleMgr.ReadDataForUI(MainWindow.FilePath, dispatcher);
                        break;

                    case "周五":
                        MainWindow.FilePath = Environment.CurrentDirectory + "\\ScheduleSets\\" + "Friday.json";
                        ScheduleMgr.ReadDataForUI(MainWindow.FilePath, dispatcher);
                        break;

                    case "周六":
                        MainWindow.FilePath = Environment.CurrentDirectory + "\\ScheduleSets\\" + "Saturday.json";
                        ScheduleMgr.ReadDataForUI(MainWindow.FilePath, dispatcher);
                        break;

                    case "周日":
                        MainWindow.FilePath = Environment.CurrentDirectory + "\\ScheduleSets\\" + "Sunday.json";
                        ScheduleMgr.ReadDataForUI(MainWindow.FilePath, dispatcher);
                        break;

                    default:
                        MainWindow.FilePath = Environment.CurrentDirectory + "\\ScheduleSets\\" + DateSelection.SelectedItem + ".json";
                        ScheduleMgr.ReadDataForUI(MainWindow.FilePath, dispatcher);
                        break;
                }
            if (this.IsLoaded == true)
            {
                ClassListView.UpdateLayout();
            }

        }

        private void DatePicker_DatePicked(DatePickerFlyout sender, DatePickedEventArgs args)
        {
            string SelectedDate = DatePicker.Date.Month + "." + DatePicker.Date.Day;
            DateSelection.Items.Add(SelectedDate);
            DateSelection.SelectedItem = SelectedDate;
        }
    }
}