namespace Photography.JsonImport
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Data.Interfaces;
    using Interfaces;

    public class JsonImporter
    {
        private const string JsonFilePath = "../../JsonFiles/{0}.json";
        private readonly IUnitOfWork unitOfWork;

        private JsonImporter(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public static JsonImporter Create(IUnitOfWork unitOfWork)
        {
            return new JsonImporter(unitOfWork);
        }

        public void ImportJsonFiles()
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

                    var json = File.ReadAllText(string.Format(JsonFilePath, i.FileName));
                    
                    i.ImportJson(this.unitOfWork, json);
                });
        }
    }
}