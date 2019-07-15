using Backend.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace Backend.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private MyContext _context;
        private IDeptRepository _dept;
        private IProjRepository _proj;
        private IUserRepository _user;

        public IDeptRepository Dept {
            get {
                if(_dept == null)
                {
                    _dept = new DeptRepository(_context);
                }
 
                return _dept;
            }
        }
 
        public IProjRepository Proj {
            get {
                if(_proj == null)
                {
                    _proj = new ProjRepository(_context);
                }
 
                return _proj;
            }
        }

        public IUserRepository User {
            get {
                if(_user == null)
                {
                    _user = new UserRepository(_context);
                }
 
                return _user;
            }
        } 
        
        public UnitOfWork(MyContext context)
        {
            _context = context;
        }
 
        public void Save()
        {
            _context.SaveChanges();
        }

        public IDbContextTransaction Transaction(){
            return _context.Database.BeginTransaction();
        }
    }
}