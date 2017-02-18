namespace DataExporting
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Web.Script.Serialization;
    using MassDefectDatabase.Data;
    using Newtonsoft.Json;

    class Startup
    {
        static void Main()
        {
            var db = new MassDefectDatabaseContext();

            ExportPlanetsWhichAreNotAnomalyOrigins(db);
            //ExportPeopleWhickHaveNotBeenVictims(db);
            //ExportTopAnomaly(db);

        }

        private static void ExportTopAnomaly(MassDefectDatabaseContext db)
        {
            var anomaly = db.Anomalies
                 .OrderByDescending(a => a.Victims.Count)
                 .Select(a => new
                 {
                     id = a.Id,
                     originPlanet = new { name = a.OriginPlanet.Name },
                     teleportPlanet = new { name = a.TeleportPlanet.Name },
                     victimsCount = a.Victims.Count
                 })
                 .Take(1);

            var topAnomalyAsJson = JsonConvert.SerializeObject(anomaly, Formatting.Indented);
            File.WriteAllText(@"../../anomaly.json", topAnomalyAsJson);
        }

        private static void ExportPeopleWhickHaveNotBeenVictims(MassDefectDatabaseContext db)
        {
            var persons = db.Persons
               .Where(p => !p.Anomalies.Any())
               .Select(p => new
               {
                   name = p.Name,
                   homePlanet = new { name = p.HomePlanet.Name }
               });

            var peopleAsJson = JsonConvert.SerializeObject(persons, Formatting.Indented);
            File.WriteAllText(@"../../people.json", peopleAsJson);
        }

        private static void ExportPlanetsWhichAreNotAnomalyOrigins(MassDefectDatabaseContext db)
        {
            var exportedPlanets = db.Planets
                .Where(p => !p.OriginAnomalies.Any())
                .Select(p => new
                {
                    name = p.Name
                });

            var planetsAsJson = JsonConvert.SerializeObject(exportedPlanets, Formatting.Indented);
            File.WriteAllText(@"../../planets.json", planetsAsJson);
        }
    }
}