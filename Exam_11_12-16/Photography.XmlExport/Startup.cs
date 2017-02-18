namespace Photography.XmlExport
{
    using Data;
    using Data.Interfaces;

    class Startup
    {
        static void Main()
        {
            IPhotographyContext context = new PhotographyContext();
            QueriesExecutor.Create(new UnitOfWork(context)).RunAllQueries();
        }
    }
}