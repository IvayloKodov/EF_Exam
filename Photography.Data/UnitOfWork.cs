namespace Photography.Data
{
    using Interfaces;
    using Models;
    using Models.Cameras;
    using Repositories;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IPhotographyContext context;

        public UnitOfWork(IPhotographyContext photographyContext)
        {
            this.context = photographyContext;
        }

        public virtual IRepository<Camera> CamerasRepo => new EfRepository<Camera>(this.context.Cameras);

        public virtual IRepository<Accessory> AccessoriesRepo => new EfRepository<Accessory>(this.context.Accessories);

        public virtual IRepository<Len> LensRepo => new EfRepository<Len>(this.context.Lens);

        public virtual IRepository<Photographer> PhotographersRepo => new EfRepository<Photographer>(this.context.Photographers);

        public virtual IRepository<Workshop> WorkshopsRepo => new EfRepository<Workshop>(this.context.Workshops);

        public void Dispose()
        {
            this.context.Dispose();
        }

        public int Save()
        {
            return this.context.SaveChanges();
        }
    }
}