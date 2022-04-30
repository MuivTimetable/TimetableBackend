namespace TimetableAPI.Models
{
    public class Group
    {
        [Key]
        public int Group_id { get; set; }

        [Required,MaxLength(50)]
        public string Group_name { get; set; }
    }
}
