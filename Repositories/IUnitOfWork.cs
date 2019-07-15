using Microsoft.EntityFrameworkCore.Storage;

namespace Backend.Repositories
{
    public interface IUnitOfWork
    {
        IDeptRepository Dept { get; }
        IProjRepository Proj { get; }
        IUserRepository User { get; }
        void Save(); 
        IDbContextTransaction Transaction();
    }
}