namespace Photography.JsonExport.Queries
{
    using System;
    using System.Linq;
    using Data;

    public class OrderedPhotographers
    {
        public void Execute()
        {
            var db = new UnitOfWork(new PhotographyContext());
            var photographers =
                db.PhotographersRepo
                    .GetAll()
                    .Select(p => new
                    {
                        p.FirstName,
                        p.LastName,
                        p.Phone
                    })
                    .OrderBy(p => p.FirstName)
                    .ThenByDescending(p => p.LastName);

            var json = ExportJsonToFile.ExportQueryToFile(photographers, "../../JsonsExports/photographers-ordered.json");
            Console.WriteLine(json);
        }
    }
}