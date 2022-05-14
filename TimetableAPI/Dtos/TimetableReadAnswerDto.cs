namespace TimetableAPI.Dtos
{
    public class TimetableReadAnswerDto
    {
        public bool success { get; set; }

        public int day_id { get; set; }
        public string date { get; set; }
        public string workDay { get; set; }
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
