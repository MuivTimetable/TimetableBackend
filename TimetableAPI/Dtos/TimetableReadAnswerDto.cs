namespace TimetableAPI.Dtos
{
    public class TimetableReadAnswerDto
    {
        public int day_id { get; set; }
        public int Work_Year { get; set; }
        public int Work_Month { get; set; }
        public int Work_Day { get; set; }
        public string? Work_Date_Name { get; set; }
        public SchedulersInDays[] Schedulers { get; set; }
    }

    public class SchedulersInDays
    {
        public int scheduler_id { get; set; }
        public string workStart { get; set; }
        public string workEnd { get; set; }
        public string area { get; set; }
        public string workType { get; set; }
        public string place { get; set; }
        public string tutor { get; set; }
        public string cathedra { get; set; }
        public string comment { get; set; }
        public int totalizer { get; set; }
    }
}
