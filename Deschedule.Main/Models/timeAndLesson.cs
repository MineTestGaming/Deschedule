using Newtonsoft.Json;
using System.IO;
using System.Text.Json;

namespace Deschedule.Main.Models
{
    public class Schedule
    {

        public required int startHr { get; set; }
        public required int startMin { get; set; }
        public required int endHr { get; set; }
        public required int endMin { get; set; }
        public required string lessonType { get; set; }
    }

    public class ScheduleMgr
    {
        public static List<Schedule> Schedules = new List<Schedule>();

        public static void GenerateDummyData()
        {
            Schedules.Add(new Schedule { startHr = 07, startMin = 50, endHr = 08, endMin = 30, lessonType = "生物" });
            Schedules.Add(new Schedule { startHr = 08, startMin = 40, endHr = 09, endMin = 20, lessonType = "生物" });
            Schedules.Add(new Schedule { startHr = 09, startMin = 40, endHr = 10, endMin = 20, lessonType = "生物" });
            Schedules.Add(new Schedule { startHr = 10, startMin = 30, endHr = 11, endMin = 10, lessonType = "生物" });
            Schedules.Add(new Schedule { startHr = 11, startMin = 20, endHr = 12, endMin = 00, lessonType = "生物" });
            Schedules.Add(new Schedule { startHr = 14, startMin = 10, endHr = 14, endMin = 55, lessonType = "生物" });
            Schedules.Add(new Schedule { startHr = 15, startMin = 05, endHr = 15, endMin = 45, lessonType = "生物" });
            Schedules.Add(new Schedule { startHr = 15, startMin = 55, endHr = 16, endMin = 35, lessonType = "生物" });
            Schedules.Add(new Schedule { startHr = 16, startMin = 45, endHr = 17, endMin = 25, lessonType = "生物" });

        }

        public static List<Schedule> LoadSchedule()
        {
            return Schedules;
        }

        public static List<DisplaySchedule> ConvertSchedule()
        {
            List<DisplaySchedule> displaySchedules = [];
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
            Schedules = JsonConvert.DeserializeObject<List<Schedule>>(content);
        }
    }

    public class DisplaySchedule
    {
        public required string Time { get; set; }
        public required string Type { get; set; }
    }

    
}
