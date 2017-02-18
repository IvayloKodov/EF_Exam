namespace Photography.JsonImport
{
    using Data;
    using Data.Interfaces;

    class Startup
    {
        static void Main()
        {
            IPhotographyContext context = new PhotographyContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);

            JsonImporter.Create(unitOfWork).ImportJsonFiles();
        }
    }
}