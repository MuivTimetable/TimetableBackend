namespace TimetableAPI.Services
{
    public class SMTPConfig
    {
        public const string SectionName = "SMTPConfig";

        public string EmailAdress { get; set; }

        public string Password { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public bool SSL { get; set; }
    }
}
