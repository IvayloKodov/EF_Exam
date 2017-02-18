namespace MassDefectDatabase.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Anomaly
    {
        private ICollection<Person> victims;

        public Anomaly()
        {
            this.victims = new HashSet<Person>();
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("OriginPlanet"), Column(Order = 0)]
        public int OriginPlanetId { get; set; }

        [InverseProperty("OriginAnomalies")]
        public virtual Planet OriginPlanet { get; set; }

        [ForeignKey("TeleportPlanet"), Column(Order = 1)]
        public int TeleportPlanetId { get; set; }

        [InverseProperty("TeleportAnomalies")]
        public virtual Planet TeleportPlanet { get; set; }

        public virtual ICollection<Person> Victims
        {
            get { return this.victims; }
            set { this.victims = value; }
        }
    }
}