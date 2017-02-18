namespace ImportingDataFromJson
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Web.Script.Serialization;
    using MassDefectDatabase.Data;
    using MassDefectDatabase.Models;
    using Models;

    public class Startup
    {
        public static void Main()
        {
            ImportSolarSystems();
            ImportStars();
            ImportPlanets();
            ImportPersons();
            ImportAnomalies();
            ImportAnomalyVictims();
        }

        private static void ImportAnomalyVictims()
        {
            var json = File.ReadAllText(FilePaths.AnomalyVictimsPath);
            var jsonSerializer = new JavaScriptSerializer();
            var parsedAnomalyVictims = jsonSerializer.Deserialize<AnomalyVictim[]>(json);

            foreach (var anomalyVictim in parsedAnomalyVictims)
            {
                ImportAnomalyVictimToDatabase(anomalyVictim);
            }
        }

        private static void ImportAnomalyVictimToDatabase(AnomalyVictim anomalyVictim)
        {
            if (anomalyVictim.Person == null || anomalyVictim.Id == 0)
            {
                throw new ArgumentNullException("Person cannot be null and Id cannot be 0!");
            }

            using (var db = new MassDefectDatabaseContext())
            {
                var anomaly = db.Anomalies.FirstOrDefault(a => a.Id == anomalyVictim.Id);

                var victim = db.Persons.FirstOrDefault(p => p.Name == anomalyVictim.Person);

                if (anomaly != null && victim != null)
                {
                    anomaly.Victims.Add(victim);

                    db.SaveChanges();
                }
            }
        }

        private static void ImportAnomalies()
        {
            var json = File.ReadAllText(FilePaths.AnomaliesPath);
            var jsonSerializer = new JavaScriptSerializer();
            var parsedAnomalies = jsonSerializer.Deserialize<AnomalyDto[]>(json);

            foreach (var anomalyDto in parsedAnomalies)
            {
                try
                {
                    ImportAnomalyToDatabase(anomalyDto);
                    Console.WriteLine($"Successfully imported anomaly.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: Invalid data.");
                }
            }
        }

        private static void ImportAnomalyToDatabase(AnomalyDto anomalyDto)
        {
            if (anomalyDto.OriginPlanet == null)
            {
                throw new ArgumentNullException("Origin planet cannot be null!");
            }
            else if (anomalyDto.TeleportPlanet == null)
            {
                throw new ArgumentNullException("Teleport planet cannot be null!");
            }

            using (var db = new MassDefectDatabaseContext())
            {
                var originPlanet = db.Planets.FirstOrDefault(p => p.Name == anomalyDto.OriginPlanet);

                var teleportPlanet = db.Planets.FirstOrDefault(p => p.Name == anomalyDto.TeleportPlanet);

                if (originPlanet == null || teleportPlanet == null)
                {
                    throw new ArgumentNullException("Origin and teleport planet doesn't exist in dbo.Planets");
                }

                var newAnomaly = new Anomaly()
                {
                    OriginPlanet = originPlanet,
                    TeleportPlanet = teleportPlanet
                };

                db.Anomalies.Add(newAnomaly);

                db.SaveChanges();
            }
        }

        private static void ImportPersons()
        {
            var json = File.ReadAllText(FilePaths.PersonsPath);
            var jsonSerializer = new JavaScriptSerializer();
            var parsedPersons = jsonSerializer.Deserialize<PersonDto[]>(json);

            foreach (var personDto in parsedPersons)
            {
                try
                {
                    ImportPersonToDatabase(personDto);
                    Console.WriteLine($"Successfully imported Person {personDto.Name}.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: Invalid data.");
                }
            }
        }

        private static void ImportPersonToDatabase(PersonDto personDto)
        {
            if (personDto == null)
            {
                throw new ArgumentNullException("Person cannot be null!");
            }
            else if (personDto.Name == null)
            {
                throw new ArgumentNullException("Person name cannot be null!");
            }
            else if (personDto.HomePlanet == null)
            {
                throw new ArgumentNullException("Person home planet cannot be null!");
            }

            using (var db = new MassDefectDatabaseContext())
            {
                var homePlanet = db.Planets.FirstOrDefault(p => p.Name == personDto.HomePlanet);

                if (homePlanet == null)
                {
                    throw new ArgumentNullException("Person home planet doesn't exist in dbo.Planets");
                }

                var newPerson = new Person()
                {
                    Name = personDto.Name,
                    HomePlanet = homePlanet
                };

                db.Persons.Add(newPerson);

                db.SaveChanges();
            }
        }

        private static void ImportPlanets()
        {
            var json = File.ReadAllText(FilePaths.PlanetsPath);
            var jsonSerializer = new JavaScriptSerializer();
            var parsedPlanets = jsonSerializer.Deserialize<PlanetDto[]>(json);

            foreach (var planetDto in parsedPlanets)
            {
                try
                {
                    ImportPlanetToDatabase(planetDto);
                    Console.WriteLine($"Successfully imported Planet {planetDto.Name}.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: Invalid data.");
                }
            }
        }

        private static void ImportPlanetToDatabase(PlanetDto planetDto)
        {
            var db = new MassDefectDatabaseContext();

            if (planetDto == null)
            {
                throw new ArgumentNullException("Planet cannot be null!");
            }
            else if (planetDto.Name == null)
            {
                throw new ArgumentNullException("Planet name cannot be null!");
            }
            else if (planetDto.Sun == null)
            {
                throw new ArgumentNullException("Planet sun cannot be null!");
            }
            else if (planetDto.SolarSystem == null)
            {
                throw new ArgumentNullException("Planet solar system cannot be null!");
            }

            var sun = db.Stars.FirstOrDefault(s => s.Name == planetDto.Sun);

            var solarSystem = db.SolarSystems.FirstOrDefault(s => s.Name == planetDto.SolarSystem);

            if (sun == null)
            {
                throw new ArgumentNullException("Sun doesn't exist in dbo.Stars");
            }
            else if (solarSystem == null)
            {
                throw new ArgumentNullException("Solar system doesn't exist in dbo.SolarSystems");
            }

            var newPlanet = new Planet()
            {
                Name = planetDto.Name,
                Sun = sun,
                SolarSystem = solarSystem
            };

            db.Planets.Add(newPlanet);

            db.SaveChanges();
        }

        private static void ImportStars()
        {
            var json = File.ReadAllText(FilePaths.StarsPath);
            var jsonSerializer = new JavaScriptSerializer();
            var parsedStars = jsonSerializer.Deserialize<StarDto[]>(json);

            foreach (var starDto in parsedStars)
            {
                try
                {
                    ImportStarToDatabase(starDto);
                    Console.WriteLine($"Successfully imported Star {starDto.Name}.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: Invalid data.");
                }
            }
        }

        private static void ImportStarToDatabase(StarDto starDto)
        {
            var db = new MassDefectDatabaseContext();

            if (starDto == null)
            {
                throw new ArgumentNullException("Star cannot be null!");
            }
            else if (starDto.Name == null)
            {
                throw new ArgumentNullException("StarName cannot be null!");
            }
            else if (starDto.SolarSystem == null)
            {
                throw new ArgumentNullException("SolarSystem cannot be null!");
            }

            var solarSystem = db.SolarSystems.FirstOrDefault(s => s.Name == starDto.SolarSystem);

            if (solarSystem == null)
            {
                throw new ArgumentNullException("Solar system doesn't exists!");
            }

            var newStar = new Star()
            {
                Name = starDto.Name,
                SolarSystem = solarSystem
            };

            db.Stars.Add(newStar);

            db.SaveChanges();
        }

        private static void ImportSolarSystems()
        {
            var json = File.ReadAllText(FilePaths.SolarSystemsPath);
            var jsonSerializer = new JavaScriptSerializer();
            var parsedSolarSystems = jsonSerializer.Deserialize<SolarSystemDto[]>(json);

            foreach (var solarSystemDto in parsedSolarSystems)
            {
                try
                {
                    ImportSolarSystemToDatabase(solarSystemDto);
                    Console.WriteLine($"Successfully imported Solar System {solarSystemDto.Name}.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: Invalid data.");
                }
            }
        }

        private static void ImportSolarSystemToDatabase(SolarSystemDto solarSystemDto)
        {
            if (solarSystemDto?.Name == null)
            {
                throw new ArgumentNullException("Name cannot be null!");
            }

            SolarSystem newSolarSystem = new SolarSystem()
            {
                Name = solarSystemDto.Name
            };

            using (var db = new MassDefectDatabaseContext())
            {
                db.SolarSystems.Add(newSolarSystem);
                db.SaveChanges();
            }
        }
    }
}
