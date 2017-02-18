namespace ImportingDataFromJson.Models
{
    public class PlanetDto
    {
        public string Name { get; set; }

        public string Sun { get; set; }

        public string SolarSystem { get; set; }

        public virtual string[] Persons { get; set; }
    }
}