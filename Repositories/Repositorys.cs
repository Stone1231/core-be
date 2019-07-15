using Backend.Models;

namespace Backend.Repositories
{
    public class DeptRepository : RepositoryBase<Dept>, IDeptRepository
    {
        public DeptRepository(MyContext context)
            : base(context)
        {
        }
    }

    public class ProjRepository : RepositoryBase<Proj>, IProjRepository
    {
        public ProjRepository(MyContext context)
            : base(context)
        {
        }
    }

    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(MyContext context)
            : base(context)
        {
        }
    } 
}