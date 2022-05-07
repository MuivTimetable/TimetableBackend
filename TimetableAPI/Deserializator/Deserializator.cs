using System.Text.Json;

namespace TimetableAPI.Deserializator
{

    public class Rootobject
    {
        public Sheduler[]? sheduler { get; set; }
    }

    public class Sheduler
    {
        public string? branch { get; set; }
        public int workYear { get; set; }
        public int workMonth { get; set; }
        public int workDate { get; set; }
        public string? workDateName { get; set; }
        public string? workDay { get; set; }
        public Worksheduler[]? workSheduler { get; set; }
    }

    public class Worksheduler
    {
        public string? workStart { get; set; }
        public string? workEnd { get; set; }
        public string? area { get; set; }
        public string? workType { get; set; }
        public string? place { get; set; }
        public string? tutor { get; set; }
        public string? cathedra { get; set; }
        public Group[]? groups { get; set; }
    }

    public class Group
    {
        public string? groupNum { get; set; }
    }

    public class Deserializator
    {
        public static void shedulerDeserializator()
        {
            string? _filePath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            string _fileName = "/shedulerData.json";
            string jsonString = File.ReadAllText(_filePath + "/sheduler" + _fileName);

            Sheduler? sheduler = JsonSerializer.Deserialize<Sheduler>(jsonString);
        }
    }
}
