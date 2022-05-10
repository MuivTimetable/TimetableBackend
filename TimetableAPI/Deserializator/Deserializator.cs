using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
namespace TimetableAPI.Deserializator
{
    //all of this next is the Sheduler json model
    public class Rootobject
    {
        public Sheduler[]? sheduler { get; set; }
    }

    public class Sheduler
    {
        public int workYear { get; set; }
        public int workMonth { get; set; }
        public int workDate { get; set; }
        public string? workDateName { get; set; }
        public string? workDay { get; set; }
        public Worksheduler[]? workSheduler { get; set; }
    }

    public class Worksheduler
    {
        public string? branch { get; set; }
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

    //next is the json model of the doc name and his date
    public class Rootnameanddate
    { 
        public Nameanddate[]? nameAndDate { get; set; }
    }

    public class Nameanddate
    {
        public string? name { get; set; }
        public string? date { get; set; }
    }

    //module of Deserialization
    public class Deserializator
    {
        public int awaitAccord = 0;
        public void shedulerDeserializator()
        {
            
            string? _debugPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);

            string nameAndDateJsonString = _debugPath + "\\NameAndDate\\Nameanddate.json";
            var nameAndDate =JsonConvert.DeserializeObject<Rootnameanddate>(File.ReadAllText(nameAndDateJsonString));

            DirectoryInfo _dirPath = new DirectoryInfo(_debugPath + "/sheduler");
            
            foreach (FileInfo _file in _dirPath.GetFiles()) 
            { 
                string lastWriteTime = _file.LastWriteTime.ToString();
                for (int i = 0; i < nameAndDate.nameAndDate.Length; i++) 
                {
                     if (_file.Name == nameAndDate.nameAndDate[i].name) 
                     {
                         if (lastWriteTime != nameAndDate.nameAndDate[i].date)
                         {
                            awaitAccord = 1;
                            string shedulerJsonString = _debugPath + "/sheduler/" + _file.Name;
                            Rootobject? sheduler = JsonConvert.DeserializeObject<Rootobject>(File.ReadAllText(shedulerJsonString));
                        } break;
                     }
                     else
                     {
                        awaitAccord = 2;
                        string shedulerJsonString = _debugPath + "/sheduler/" + _file.Name;
                        Rootobject? sheduler = JsonConvert.DeserializeObject<Rootobject>(File.ReadAllText(shedulerJsonString));
                     }
                }
                switch (awaitAccord)
                {
                    case 1:
                        //тута идёт обновление БД
                        
                        break;

                    case 2:
                        //тута заполняется БД как и надо
                        
                        break;

                    default: 
                        
                        break;
                }
            }
        }
    }
}
/*
 * string jsonString = File.ReadAllText(_debugPath + "/sheduler" + _fileName.Name);

                Sheduler? sheduler = JsonConvert.DeserializeObject<Sheduler>(jsonString);
 */