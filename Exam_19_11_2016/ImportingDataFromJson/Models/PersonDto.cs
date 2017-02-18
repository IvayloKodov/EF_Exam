namespace ImportingDataFromJson.Models
{
    public class PersonDto
    {
        public string Name { get; set; }

        public string HomePlanet { get; set; }

        public virtual string[] Anomalies { get; set; }
    }
}