namespace Photography.XmlImport
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Data;
    using Data.Interfaces;
    using Interfaces;

    public class XmlsImporter
    {
        private readonly IUnitOfWork unitOfWork;

        private XmlsImporter(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public static XmlsImporter Create(IUnitOfWork unitOfWork)
        {
            return new XmlsImporter(unitOfWork);
        }

        public void ImportAllXmls()
        {
            Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(IImport).IsAssignableFrom(t)
                && !t.IsAbstract && !t.IsInterface)
                .Select(Activator.CreateInstance)
                .OfType<IImport>()
                .OrderBy(i => i.Order)
                .ToList()
                .ForEach(i =>
                {
                    Console.WriteLine(i.Message);
                    var reader = new StreamReader(i.FilePath);
                    i.Import(this.unitOfWork, reader);
                });
        }
    }
}