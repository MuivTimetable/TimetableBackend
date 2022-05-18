﻿using System.Text.Json;
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
        public int groupCode { get; set; } 
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

                                        var schedulerLINQ = _context.Schedulers.Select(s => new { s.Scheduler_id }).ToList().Last().ToString();
                                        var schedulerID = Int32.Parse(schedulerLINQ);
                                        
                                        for (int h = 0; h < sheduler.sheduler[i].workSheduler[j].groups.Length; h++)
                                        {
                                            _context.Schedulers_Groups.Add(new Models.Scheduler_Group
                                            {
                                                Scheduler_id = schedulerID,
                                                Group_id = sheduler.sheduler[i].workSheduler[j].groups[h].groupCode
                                            });
                                        }
                                    }
                                }
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
