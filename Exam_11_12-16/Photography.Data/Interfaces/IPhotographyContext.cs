namespace Photography.Data.Interfaces
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using Models;
    using Models.Cameras;

    public interface IPhotographyContext
    {
        DbSet<Camera> Cameras { get; set; }

        DbSet<Accessory> Accessories { get; set; }

        DbSet<Len> Lens { get; set; }

        DbSet<Photographer> Photographers { get; set; }

        DbSet<Workshop> Workshops { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        void Dispose();

        int SaveChanges();
    }
}