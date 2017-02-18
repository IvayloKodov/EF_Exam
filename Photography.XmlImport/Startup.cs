namespace Photography.XmlImport
{
    using Data;
    using Data.Interfaces;

    class Startup
    {
        static void Main()
        {
            var context = new PhotographyContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            XmlsImporter.Create(unitOfWork).ImportAllXmls();
        }
    }
}
