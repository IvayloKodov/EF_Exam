namespace XmlDataExporting
{
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;
    using MassDefectDatabase.Data;

    class Startup
    {
        static void Main()
        {
            var db = new MassDefectDatabaseContext();

            var exportedAnomalies = db.Anomalies
                .Select(a => new
                {
                    id = a.Id,
                    originPlanetName = a.OriginPlanet.Name,
                    teleportPlanetName = a.TeleportPlanet.Name,
                    Persons = a.Victims.Select(v => v.Name)
                })
                .OrderBy(a => a.id);

            var xmlDocument = new XElement("anomalies");

            foreach (var anomaly in exportedAnomalies)
            {
                var anomalyNode = new XElement("anomaly");
                anomalyNode.Add(new XAttribute("id", anomaly.id));
                anomalyNode.Add(new XAttribute("origin-planet", anomaly.originPlanetName));
                anomalyNode.Add(new XAttribute("teleport-planet", anomaly.teleportPlanetName));


                var victimsNode = new XElement("victims");
                foreach (var person in anomaly.Persons)
                {
                    var personNode = new XElement("victim");
                    personNode.Add(new XAttribute("name",person));
                    victimsNode.Add(personNode);
                }
                anomalyNode.Add(victimsNode);
                xmlDocument.Add(anomalyNode);
            }

            xmlDocument.Save("../../anomalies.xml");
        }
    }
}
