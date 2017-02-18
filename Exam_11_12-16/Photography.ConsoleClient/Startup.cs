namespace Photography.ConsoleClient
{
    using Data;

    public class Startup
    {
        public static void Main()
        {
            var context = new PhotographyContext();
            context.Database.Initialize(true);
        }
    }
}