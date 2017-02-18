namespace ImportingDataFromXML
{
    using System;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using MassDefectDatabase.Data;
    using MassDefectDatabase.Models;

    public class Startup
    {
        private const string NewAnomaliesPath = @"..\..\..\datasets\new-anomalies.xml";

        public static void Main()
        {
            var inputXml = XDocument.Load(NewAnomaliesPath);
            var anomalies = inputXml.XPathSelectElements("/anomalies/anomaly");

            var db = new MassDefectDatabaseContext();

            foreach (XElement anomaly in anomalies)
            {
                try
                {
                    ImportAnomalyAndVictims(anomaly, db);
                    Console.WriteLine($"Successfully imported anomaly.");
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine($"Error: Invalid data.");
                }
                catch (DbEntityValidationException dbex)
                {
                    Console.WriteLine(dbex.InnerException);
                }
            }
        }

        private static void ImportAnomalyAndVictims(XElement anomalyNode, MassDefectDatabaseContext db)
        {
            XAttribute originPlanetName = anomalyNode.Attribute("origin-planet");
            XAttribute teleportPlanetName = anomalyNode.Attribute("teleport-planet");

            if (originPlanetName == null || teleportPlanetName == null)
            {
                throw new ArgumentNullException("Origin and teleport planet cannot be null!");
            }

            var originPlanet = GetPlanetByName(originPlanetName.Value, db);
            var teleportPlanet = GetPlanetByName(teleportPlanetName.Value, db);

            if (originPlanet == null || teleportPlanet == null)
            {
                throw new ArgumentNullException("Origin or teleport planet doesn't exist in dbo.Planets!");
            }

            var newAnomaly = new Anomaly()
            {
                OriginPlanet = originPlanet,
                TeleportPlanet = teleportPlanet
            };

            db.Anomalies.Add(newAnomaly);

            //Victims
            var victims = anomalyNode.XPathSelectElements("victims/victim");

            foreach (var victim in victims)
            {
                ImportVictim(victim, db, newAnomaly);
            }

            db.SaveChanges();
        }

        private static void ImportVictim(XElement victimNode, MassDefectDatabaseContext db, Anomaly newAnomaly)
        {
            XAttribute name = victimNode.Attribute("name");

            if (name == null)
            {
                throw new ArgumentNullException("There is no attribute for victim name in xml!");
            }

            var person = GetPersonByName(name.Value, db);

            if (person == null)
            {
                throw new ArgumentNullException("There is no such person in dbo.Persons");
            }

            newAnomaly.Victims.Add(person);
        }

        private static Person GetPersonByName(string personName, MassDefectDatabaseContext db)
        {
            return db.Persons.FirstOrDefault(p => p.Name == personName);
        }

        private static Planet GetPlanetByName(string planetName, MassDefectDatabaseContext db)
        {
            if (planetName == null)
            {
                throw new ArgumentNullException("Planet name cannot be null!");
            }

            return db.Planets.FirstOrDefault(p => p.Name == planetName);
        }
    }
}