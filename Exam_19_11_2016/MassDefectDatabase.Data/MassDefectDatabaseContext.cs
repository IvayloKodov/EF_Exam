namespace MassDefectDatabase.Data
{
    using System.Data.Entity;
    using Models;

    public class MassDefectDatabaseContext : DbContext
    {
        public MassDefectDatabaseContext()
                  : base("name=MassDefectDatabaseContext")
        {
        }

        public virtual IDbSet<Anomaly> Anomalies { get; set; }

        public virtual IDbSet<Person> Persons { get; set; }

        public virtual IDbSet<Planet> Planets { get; set; }

        public virtual IDbSet<SolarSystem> SolarSystems { get; set; }

        public virtual IDbSet<Star> Stars { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anomaly>()
                .HasMany(a => a.Victims)
                .WithMany(p => p.Anomalies)
                .Map(m =>
                {
                    m.MapLeftKey("AnomalyId");
                    m.MapRightKey("PersonId");
                    m.ToTable("AnomalyVictims");
                });

            modelBuilder.Entity<Anomaly>()
                .HasRequired(a => a.OriginPlanet)
                .WithMany(a => a.OriginAnomalies)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Anomaly>()
                .HasRequired(a => a.TeleportPlanet)
                .WithMany(a => a.TeleportAnomalies)
                .WillCascadeOnDelete(false);


            base.OnModelCreating(modelBuilder);
        }
    }
}