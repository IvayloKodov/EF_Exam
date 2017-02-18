namespace Photography.JsonExport.Queries
{
    using System;
    using System.Linq;
    using Data;
    using Models.Cameras;

    public class LandscapePhotographers
    {
        public void Execute()
        {
            var db = new UnitOfWork(new PhotographyContext());
            var photographers =
                db.PhotographersRepo
                    .GetAll()
                    .ToList()
                    .Where(p => p.PrimaryCamera.GetType() == typeof(DslrCamera))
                    .Select(p => new
                    {
                        p.FirstName,
                        p.LastName,
                        CameraMake=p.PrimaryCamera.Make,
                        LensesCount = p.Lens.Count(l => l.FocalLength<=30)
                    });

            var json = ExportJsonToFile.ExportQueryToFile(photographers, "../../JsonsExports/photographers-ordered.json");
            Console.WriteLine(json);
        }
    }
}