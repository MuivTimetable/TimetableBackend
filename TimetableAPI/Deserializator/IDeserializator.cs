namespace TimetableAPI.Deserializator
{
    public interface IDeserializator
    {
        string ShedulerDeserializator();

        void DBContentRemover();
    }
}
