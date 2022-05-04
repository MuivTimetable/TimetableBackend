namespace TimetableAPI.Dtos
{
    public class UserAutoRequestDto
    {
        public string Login { get; set; }

        //TODO: hash
        public string Password { get; set; }

        //TODO: Определить перечнь прочих требований
    }
}
