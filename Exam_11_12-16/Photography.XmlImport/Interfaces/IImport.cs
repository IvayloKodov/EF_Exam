namespace Photography.XmlImport.Interfaces
{
    using System;
    using System.IO;
    using Data.Interfaces;

    public interface IImport
    {
        string FilePath { get; }

        int Order { get; }

        string Message { get; }

        Action<IUnitOfWork, StreamReader> Import { get; }
    }
}