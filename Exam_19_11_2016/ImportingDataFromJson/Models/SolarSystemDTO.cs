namespace ImportingDataFromJson.Models
{
    public class SolarSystemDto
    {
        public string Name { get; set; }

        public virtual string[] Stars { get; set; }

        public virtual string[] Planets { get; set; }
    }
}