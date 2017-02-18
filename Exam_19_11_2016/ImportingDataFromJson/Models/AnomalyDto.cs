namespace ImportingDataFromJson.Models
{
    public class AnomalyDto
    {
        public string OriginPlanet { get; set; }

        public string TeleportPlanet { get; set; }

        public virtual string[] Victims { get; set; }
    }
}