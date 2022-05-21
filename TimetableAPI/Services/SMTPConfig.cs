using System.Text;

namespace TimetableAPI.Services
{
    public class SMTPConfig
    {
        public const string SectionName = "SMTPConfig";

        private string emailAdress;

        public string EmailAdress {
            get
            {
                return emailAdress;
            }
            set
            {
                string[] outArray = value.Split(',');
                byte[] outByte = new byte[outArray.Length-1];
                for (int i = 0; i < outArray.Length - 1; i++)
                {
                    outByte[i] = Convert.ToByte(outArray[i]);
                }
                emailAdress = Encoding.Default.GetString(outByte);
            }
        }

        private string password;

        public string Password { 
            get 
            {
                return password;
            } 
            set
            {
                string[] outArray = value.Split(',');
                byte[] outByte = new byte[outArray.Length-1];
                for (int i = 0; i < outArray.Length - 1; i++)
                {
                    outByte[i] = Convert.ToByte(outArray[i]);
                }
                password = Encoding.Default.GetString(outByte);
            } }

        public string Host { get; set; }

        public int Port { get; set; }

        public bool SSL { get; set; }
    }
}
