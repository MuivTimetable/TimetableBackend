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
        public string? groupCode { get; set; } 
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
        public string ShedulerDeserializator()
        {

            string? _debugPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);

            string deepLevel = "/../../../../../../data/sheduler";

            if (!Directory.Exists(_debugPath + deepLevel + "/NameAndDate"))
            {
                Directory.CreateDirectory(_debugPath + deepLevel + "/NameAndDate");
            }
            if (!Directory.Exists(_debugPath + deepLevel + "/sheduler"))
            {
                Directory.CreateDirectory(_debugPath + deepLevel + "/sheduler");
            }
            if (!File.Exists($"{_debugPath}{deepLevel}/NameAndDate/Nameanddate.json"))
            {
                var nameanddateJoson = new Rootnameanddate
                {
                    nameAndDate = new Nameanddate[]
                    {
                        new Nameanddate()
                        {
                            name = "shedulerDataTest.json",
                            date = "2008-03-09T16:05:07"
                        },
                        new Nameanddate()
                        {
                            name = "shedulerDataTest2.json",
                            date = "2008-03-09T17:05:07"
                        }
                    }
                };
                using (var stream = new FileStream($"{_debugPath}{deepLevel}/NameAndDate/Nameanddate.json", FileMode.OpenOrCreate))
                {
                    using( var sw = new StreamWriter(stream))
                    {
                        sw.Write(JsonConvert.SerializeObject(nameanddateJoson));
                    }
                }
            }

            string nameAndDateJsonString = _debugPath + deepLevel + "/NameAndDate/Nameanddate.json";
            var nameAndDate =JsonConvert.DeserializeObject<Rootnameanddate>(File.ReadAllText(nameAndDateJsonString));

            DirectoryInfo _dirPath = new DirectoryInfo(_debugPath + deepLevel + "/sheduler");
            /*while (cycleIsTrue == true)
            {
                if (DateTime.UtcNow.Minute == 0 && DateTime.UtcNow.Second == 0)
                {*/
                    foreach (FileInfo _file in _dirPath.GetFiles())
                    {
                        string shedulerJsonString = _debugPath + deepLevel + "/sheduler/" + _file.Name;
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
                                else if (lastWriteTime == nameAndDate.nameAndDate[i].date) { awaitAccord = 0; }
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

                                for (int i = 0; i < sheduler.sheduler.Length; i++)
                                {

                                    string stringActualDayId = Convert.ToString(sheduler.sheduler[i].workDate) + Convert.ToString(sheduler.sheduler[i].workMonth) + Convert.ToString(sheduler.sheduler[i].workYear);
                                    int intActualDayID = Int32.Parse(stringActualDayId);

                                    var deleteContent = _context.SchedulerDates.Where(k => k.Day_id == intActualDayID).ToList();
                                    _context.SchedulerDates.RemoveRange(deleteContent);
                                    _context.SchedulerDates.Add(new Models.SchedulerDate
                                    {
                                        Day_id = intActualDayID,
                                        Work_Date_Name = sheduler.sheduler[i].workDateName,
                                        Work_Day = sheduler.sheduler[i].workDate,
                                        Work_Month = sheduler.sheduler[i].workMonth,
                                        Work_Year = sheduler.sheduler[i].workYear
                                    });
                                    _context.SaveChanges();
                                    
                                    for (int j = 0; j < sheduler.sheduler[i].workSheduler.Length; j++)
                                    {

                                        _context.Schedulers.Add(new Models.Scheduler
                                        {
                                            Day_id = intActualDayID,
                                            Branch = sheduler.sheduler[i].workSheduler[j].branch,
                                            Work_start = sheduler.sheduler[i].workSheduler[j].workStart,
                                            Work_end = sheduler.sheduler[i].workSheduler[j].workEnd,
                                            Area = sheduler.sheduler[i].workSheduler[j].area,
                                            Work_type = sheduler.sheduler[i].workSheduler[j].workType,
                                            Place = sheduler.sheduler[i].workSheduler[j].place,
                                            Tutor = sheduler.sheduler[i].workSheduler[j].tutor,
                                            Cathedra = sheduler.sheduler[i].workSheduler[j].cathedra,
                                            Totalizer = 0
                                        });
                                        _context.SaveChanges();

                                        string schedulerLINQ = _context.Schedulers.OrderByDescending(s => s.Scheduler_id).Select(s => s.Scheduler_id).FirstOrDefault().ToString();
                                        int schedulerID = int.Parse(schedulerLINQ);

                                        for (int h = 0; h < sheduler.sheduler[i].workSheduler[j].groups.Length; h++)
                                        {
                                            var stringGroupID = "1" + sheduler.sheduler[i].workSheduler[j].groups[h].groupCode;
                                            var intGroupID = Int32.Parse(stringGroupID);
                                            
                                            if (_context.Groups.Where(s => s.Group_id.Equals(intGroupID)).Select(s => s.Group_id).Any()) 
                                            {
                                                _context.Schedulers_Groups.Add(new Models.Scheduler_Group
                                                {
                                                    Scheduler_id = schedulerID,
                                                    Group_id = intGroupID
                                                });
                                            }
                                        }
                                    }
                                }
                                _context.SaveChanges();
                                
                                break;

                            case 2:
                                //тут заполняется БД как и надо
                                
                                for (int i = 0; i < sheduler.sheduler.Length; i++)
                                {
                                    
                                    string stringActualDayId = Convert.ToString(sheduler.sheduler[i].workDate) + Convert.ToString(sheduler.sheduler[i].workMonth) + Convert.ToString(sheduler.sheduler[i].workYear);
                                    int intActualDayID = Int32.Parse(stringActualDayId);
                                    _context.SchedulerDates.Add(new Models.SchedulerDate 
                                    { 
                                        Day_id = intActualDayID,
                                        Work_Date_Name = sheduler.sheduler[i].workDateName, 
                                        Work_Day = sheduler.sheduler[i].workDate, 
                                        Work_Month = sheduler.sheduler[i].workMonth, 
                                        Work_Year = sheduler.sheduler[i].workYear 
                                    });
                                    
                                    for (int j = 0; j < sheduler.sheduler[i].workSheduler.Length; j++)
                                    {

                                        _context.Schedulers.Add(new Models.Scheduler 
                                        { 
                                            Day_id = intActualDayID,
                                            Branch = sheduler.sheduler[i].workSheduler[j].branch,
                                            Work_start = sheduler.sheduler[i].workSheduler[j].workStart,
                                            Work_end = sheduler.sheduler[i].workSheduler[j].workEnd,
                                            Area = sheduler.sheduler[i].workSheduler[j].area,
                                            Work_type = sheduler.sheduler[i].workSheduler[j].workType,
                                            Place = sheduler.sheduler[i].workSheduler[j].place,
                                            Tutor = sheduler.sheduler[i].workSheduler[j].tutor,
                                            Cathedra = sheduler.sheduler[i].workSheduler[j].cathedra,
                                            Totalizer = 0
                                        });
                                        _context.SaveChanges();

                                        string schedulerLINQ = _context.Schedulers.OrderByDescending(s => s.Scheduler_id).Select(s => s.Scheduler_id).FirstOrDefault().ToString();
                                        int schedulerID = int.Parse(schedulerLINQ);
                                        
                                        for (int h = 0; h < sheduler.sheduler[i].workSheduler[j].groups.Length; h++)
                                        {
                                            var stringGroupID = "1" + sheduler.sheduler[i].workSheduler[j].groups[h].groupCode;
                                            var intGroupID = int.Parse(stringGroupID);
                                            
                                            if (_context.Groups.Where(s => s.Group_id.Equals(intGroupID)).Select(s => s.Group_id).Any()) 
                                            {
                                                _context.Schedulers_Groups.Add(new Models.Scheduler_Group
                                                {
                                                    Scheduler_id = schedulerID,
                                                    Group_id = intGroupID
                                                });
                                            }
                                        }
                                    }
                                }
                                _context.SaveChanges();
                                
                                break;

                            default:

                                break;
                        }
                        Nameanddate newNameAndDate = new Nameanddate
                        {
                            name = _file.Name,
                            date = lastWriteTime
                        };
                        string jsonNewNameAndDate = JsonConvert.SerializeObject(newNameAndDate);

                        string str = "},";
                                
                        string nameAndDateContent = File.ReadAllText(nameAndDateJsonString);
                        int indexOfString = nameAndDateContent.IndexOf(str);
                        nameAndDateContent = nameAndDateContent.Insert(136, jsonNewNameAndDate + ",");
                                
                        File.WriteAllText(nameAndDateJsonString, nameAndDateContent);

                        File.Delete(shedulerJsonString);
                    }
            return awaitAccord.ToString();
                //}
            //}
        }
        public void DBContentRemover()
        {
            if (DateTime.UtcNow.DayOfWeek == DayOfWeek.Monday)
            {
                for (int day = -7; day < 0; day++)
                {
                    DateTime dayToRemove = DateTime.UtcNow.AddDays(day);
                    int dayIDToRemove = Int32.Parse(dayToRemove.Day.ToString() + dayToRemove.Month.ToString() + dayToRemove.Year.ToString());

                    var deleteContent = _context.SchedulerDates.Where(k => k.Day_id == dayIDToRemove).ToList();
                    _context.SchedulerDates.RemoveRange(deleteContent);
                }
                _context.SaveChanges();
            }
        }
    }
}
