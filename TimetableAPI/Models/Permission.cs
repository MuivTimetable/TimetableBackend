namespace TimetableAPI.Models
{
    public class Permission
    {
        [Key]
        public int MyProperty { get; set; }

        [Required]
        public string Permission_name { get; set; }
    }
}
