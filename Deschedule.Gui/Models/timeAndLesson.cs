using Deschedule.Gui;
using Microsoft.UI.Dispatching;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using CommunityToolkit.Mvvm;
using System.Runtime.CompilerServices;
using Windows.Storage;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Deschedule.Main.Models
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChange([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }


    public partial class Schedule : ViewModelBase
    {
        private int id;
        private int starthr;
        private int startmin;
        private int endmin;
        private int endhr;
        private string lessontype;

        public int Id
        { get { return id; } set { id = value; OnPropertyChange(); } }
        public int startHr
        { get { return starthr; } set { starthr = value; OnPropertyChange(); } }
        public int startMin
        { get { return startmin; } set { startmin = value; OnPropertyChange(); } }
        public int endHr
        { get { return endhr; } set { endhr = value; OnPropertyChange(); } }
        public int endMin
        { get { return endmin; } set { endmin = value; OnPropertyChange(); } }
        public string lessonType
        { get { return lessontype; } set { lessontype = value; OnPropertyChange(); } }
    }

/*
    public partial class Schedule : ViewModelBase
    {
        private int id;
        private int starthr;
        private int startmin;
        private int endmin;
        private int endhr;
        private string lessontype;

        public int Id
        { get { return id; } set { id = value; } }
        public int startHr
        { get { return starthr; } set { starthr = value;  } }
        public int startMin
        { get { return startmin; } set { startmin = value;  } }
        public int endHr
        { get { return endhr; } set { endhr = value;  } }
        public int endMin
        { get { return endmin; } set { endmin = value;  } }
        public string lessonType
        { get { return lessontype; } set { lessontype = value;  } }
    }
*/
    public class ScheduleMgr
    {
        private static ObservableCollection<Schedule> schedules = new ObservableCollection<Schedule>();
        public static ObservableCollection<Schedule> Schedules { get; set; }

        public static void GenerateDummyData()
        {
            Schedules.Add(new Schedule { Id = 0, startHr = 07, startMin = 50, endHr = 08, endMin = 30, lessonType = "生物" });
            Schedules.Add(new Schedule { Id = 1, startHr = 08, startMin = 40, endHr = 09, endMin = 20, lessonType = "生物" });
            Schedules.Add(new Schedule { Id = 2, startHr = 09, startMin = 40, endHr = 10, endMin = 20, lessonType = "生物" });
            Schedules.Add(new Schedule { Id = 3, startHr = 10, startMin = 30, endHr = 11, endMin = 10, lessonType = "生物" });
            Schedules.Add(new Schedule { Id = 4, startHr = 11, startMin = 20, endHr = 12, endMin = 00, lessonType = "生物" });
            Schedules.Add(new Schedule { Id = 5, startHr = 14, startMin = 10, endHr = 14, endMin = 55, lessonType = "生物" });
            Schedules.Add(new Schedule { Id = 6, startHr = 15, startMin = 05, endHr = 15, endMin = 45, lessonType = "生物" });
            Schedules.Add(new Schedule { Id = 7, startHr = 15, startMin = 55, endHr = 16, endMin = 35, lessonType = "生物" });
            Schedules.Add(new Schedule { Id = 8, startHr = 16, startMin = 45, endHr = 17, endMin = 25, lessonType = "生物" });
        }

        public static ObservableCollection<Schedule> LoadSchedule()
        {
            return Schedules;
        }

        public static ObservableCollection<DisplaySchedule> ConvertSchedule()
        {
            ObservableCollection<DisplaySchedule> displaySchedules = [];
            foreach (Schedule schedule in Schedules)
            {
                displaySchedules.Add(new DisplaySchedule { Time = schedule.startHr.ToString("00") + ":" + schedule.startMin.ToString("00") + "-" + schedule.endHr.ToString("00") + ":" + schedule.endMin.ToString("00"), Type = schedule.lessonType.ToString() });
            }
            return displaySchedules;
        }

        public static void ImportSettings(string location)
        {
            string content = File.ReadAllText(location);
            Schedules.Clear();
            Schedules = JsonConvert.DeserializeObject<ObservableCollection<Schedule>>(content);
        }

        public static void AddData(int startHr, int startMin, int endHr, int endMin, string lessonType)
        {
            Schedules.Add(new Schedule { startHr = startHr, startMin = startMin, endHr = endHr, endMin = endMin, lessonType = lessonType });
        }

        public static void PushData(ObservableCollection<Schedule> schedules)
        {
            Schedules = schedules;
        }

        public static async void SaveData(string path)
        {
            StorageFile saveLocation;
            String folderPath = Environment.CurrentDirectory + "\\ScheduleSets";
            StorageFolder parentFolder = await StorageFolder.GetFolderFromPathAsync(folderPath);
            saveLocation = await parentFolder.CreateFileAsync(path.Replace(folderPath + "\\", String.Empty), CreationCollisionOption.ReplaceExisting);

            // CachedFileManager.DeferUpdates(saveLocation);
            await Windows.Storage.FileIO.WriteTextAsync(saveLocation, JsonConvert.SerializeObject(Schedules));
            // FileUpdateStatus fileUpdateStatus = await CachedFileManager.CompleteUpdatesAsync(saveLocation);
        }

        public static async void ReadData(string path)
        {
            if (File.Exists(path))
            {
                StorageFile openLocation;
                openLocation = await Windows.Storage.StorageFile.GetFileFromPathAsync(path);
                CachedFileManager.DeferUpdates(openLocation);
                string readContents = await FileIO.ReadTextAsync(openLocation);
                Schedules = JsonConvert.DeserializeObject<ObservableCollection<Schedule>>(readContents);
            }
            else
            {
                Schedules.Clear();
            }
        }

        public static async void ReadDataForUI(string path, DispatcherQueue dispatcherQueue)
        {
            if (File.Exists(path))
            {
                StorageFile openLocation;
                openLocation = await Windows.Storage.StorageFile.GetFileFromPathAsync(path);
                CachedFileManager.DeferUpdates(openLocation);
                string readContents = await FileIO.ReadTextAsync(openLocation);
                dispatcherQueue.TryEnqueue(() =>
                {
                    Schedules = JsonConvert.DeserializeObject<ObservableCollection<Schedule>>(readContents);
                    
                    });
            }
            else
            {
                dispatcherQueue.TryEnqueue(() =>
                Schedules.Clear());
            }
        }


    }

    public class DisplaySchedule
    {
        public string Time { get; set; }
        public string Type { get; set; }
    }
}