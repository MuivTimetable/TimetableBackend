namespace TimetableAPI.Services
{
    public class SMTPConfig
    {
        public const string SectionName = "SMTPConfig";

        public string EmailAdress { get; set; }

        public string Password { get; set; }
    }
}
