namespace TimetableAPI.Dtos
{
    public class TimetableReadAnswerDto
    {
        public IEnumerable<Timetables> Timetables { get; set; }
    }


    public class Timetables
    {
        public int Day_id { get; set; }
        public int Work_Year { get; set; }
        public int Work_Month { get; set; }
        public int Work_Day { get; set; }
        public string? Work_Date_Name { get; set; }
        public string DayOfTheWeek { get; set; }
        public List<SchedulersInDays> Schedulers { get; set; }
    }

    public class SchedulersInDays
    {
        public int Scheduler_id { get; set; }
        public string WorkStart { get; set; }
        public string WorkEnd { get; set; }
        public string Branch { get; set; }
        public string Area { get; set; }
        public string WorkType { get; set; }
        public string Place { get; set; }
        public string Tutor { get; set; }
        public string Cathedra { get; set; }
        public string Comment { get; set; }
        public int Totalizer { get; set; }
    }
}
