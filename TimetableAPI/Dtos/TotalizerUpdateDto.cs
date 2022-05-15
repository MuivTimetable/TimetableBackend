namespace TimetableAPI.Dtos
{
    public class TotalizerUpdateDto
    {
        public string Token { get; set; }

        public bool MoreOrLess { get; set; }

        public int[] Scheduler_id { get; set; }
    }
}
