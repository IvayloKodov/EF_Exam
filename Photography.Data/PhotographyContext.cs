namespace Photography.Data
{
    using System.Data.Entity;
    using Interfaces;
    using Models;
    using Models.Cameras;

    public class PhotographyContext : DbContext, IPhotographyContext
    {
        public PhotographyContext()
             : base("name=PhotographyContext")
        {
        }

        public virtual DbSet<Camera> Cameras { get; set; }

        public virtual DbSet<Accessory> Accessories { get; set; }

        public virtual DbSet<Len> Lens { get; set; }

        public virtual DbSet<Photographer> Photographers { get; set; }

        public virtual DbSet<Workshop> Workshops { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Len max aperture = floating point number precise 1 digit after decimal point
            modelBuilder.Entity<Len>()
                .Property(l => l.MaxAperture)
                .HasPrecision(10, 1);

            modelBuilder.Entity<Photographer>()
              .HasRequired(p => p.PrimaryCamera)
              .WithMany(c => c.PhotographersPrimaryCam)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<Photographer>()
              .HasRequired(p => p.SecondaryCamera)
              .WithMany(c => c.PhotographersSecondaryCam)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<Workshop>()
                .HasRequired(w=>w.Trainer)
                .WithMany(p=>p.TrainerWorkshops)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

    }
}