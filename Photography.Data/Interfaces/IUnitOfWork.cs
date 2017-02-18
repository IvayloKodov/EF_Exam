namespace Photography.Data.Interfaces
{
    using Models;
    using Models.Cameras;

    public interface IUnitOfWork
    {
        IRepository<Camera> CamerasRepo { get; }

        IRepository<Accessory> AccessoriesRepo { get; }

        IRepository<Len> LensRepo { get; }

        IRepository<Photographer> PhotographersRepo { get; }

        IRepository<Workshop> WorkshopsRepo { get; }

        void Dispose();

        int Save();
    }
}