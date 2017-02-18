namespace Photography.JsonImport.Interfaces
{
    using System;
    using Data.Interfaces;

    public interface IImport
    {
        int Order { get; }

        string Message { get; }

        string FileName { get; }

        Action<IUnitOfWork, string> ImportJson { get; }
    }
}