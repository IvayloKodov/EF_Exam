namespace Photography.XmlExport
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Data;
    using Data.Interfaces;
    using Interfaces;

    public class QueriesExecutor
    {
        private const string DirectoryPath = "../../XmlQueries/{0}.xml";

        private readonly IUnitOfWork unitOfWork;


        private QueriesExecutor(IUnitOfWork unitOfwork)
        {
            this.unitOfWork = unitOfwork;
        }

        public static QueriesExecutor Create(IUnitOfWork unitOfWork)
        {
            return new QueriesExecutor(unitOfWork);
        }

        public void RunAllQueries()
        {
            Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(IQuery).IsAssignableFrom(t) &&
                            !t.IsAbstract && !t.IsInterface)
                .Select(Activator.CreateInstance)
                .OfType<IQuery>()
                .OrderBy(q => q.Order)
                .ToList()
                .ForEach(q =>
                {
                    var writer = new StreamWriter(String.Format(DirectoryPath, q.FileName));
                    q.ExecuteQuery(this.unitOfWork, writer);
                });
        }
    }
}