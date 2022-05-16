using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
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

    public class DeserializatorLaunch
    {
        private readonly IDeserializator _deserializator;
        public DeserializatorLaunch(IDeserializator deserializator)
        {
            _deserializator = deserializator;
        }

        public void Launcher()
        {
            _deserializator.ShedulerDeserializator();
        }
    }
    public class Deserializator : IDeserializator
    {
        private readonly ApplicationContext _context;
        public Deserializator(ApplicationContext context)
        {
            _context = context;
        }

        public int awaitAccord = 0;
        protected bool cycleIsTrue = true;
        public void ShedulerDeserializator()
        {
             
            string? _debugPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);

            string nameAndDateJsonString = _debugPath + "/NameAndDate/Nameanddate.json";
            var nameAndDate =JsonConvert.DeserializeObject<Rootnameanddate>(File.ReadAllText(nameAndDateJsonString));

            DirectoryInfo _dirPath = new DirectoryInfo(_debugPath + "/sheduler");
            /*while (cycleIsTrue == true)
            {
                if (DateTime.UtcNow.Minute == 0 && DateTime.UtcNow.Second == 0)
                {*/
                    foreach (FileInfo _file in _dirPath.GetFiles())
                    {
                        string shedulerJsonString = _debugPath + "/sheduler/" + _file.Name;
                        Rootobject? sheduler = JsonConvert.DeserializeObject<Rootobject>(File.ReadAllText(shedulerJsonString));
                        string lastWriteTime = _file.LastWriteTime.ToString();
                        for (int i = 0; i < nameAndDate.nameAndDate.Length; i++)
                        {
                            if (_file.Name == nameAndDate.nameAndDate[i].name)
                            {
                                if (lastWriteTime != nameAndDate.nameAndDate[i].date)
                                {
                                    awaitAccord = 1;
                                }
                                break;
                            }
                            else
                            {
                                awaitAccord = 2;
                            }
                        }
                        switch (awaitAccord)
                        {
                            case 1:
                                //тут идёт обновление БД

                                break;

                            case 2:
                                //тут заполняется БД как и надо
                                var group = new Models.Group { Group_name = sheduler.sheduler[0].workSheduler[0].groups[0].groupNum };
                                _context.Groups.Add(group);
                                _context.SaveChanges();
                                break;

                            default:

                                break;
                        }
                    }
                //}
            //}
        }
    }
}
