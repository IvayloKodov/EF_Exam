namespace Photography.JsonExport
{
    using Queries;

    class Startup
    {
        static void Main()
        {
            var orderedProtographers = new OrderedPhotographers();
            orderedProtographers.Execute();

            var landscapePhoto = new LandscapePhotographers();
            landscapePhoto.Execute();
        }
    }
}