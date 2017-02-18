namespace MassDefectDatabase.ConsoleClient
{
    using Data;

    public class Startup
    {
        public static void Main()
        {
            var db = new MassDefectDatabaseContext();
            db.Database.Initialize(true);
        }
    }
}
