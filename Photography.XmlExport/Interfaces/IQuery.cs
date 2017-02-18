namespace Photography.XmlExport.Interfaces
{
    using System.IO;
    using Data;
    using Data.Interfaces;

    public interface IQuery
    {
        int Order { get; }

        string FileName { get; }

        void ExecuteQuery(IUnitOfWork unitOfWork, TextWriter writer);
    }
}