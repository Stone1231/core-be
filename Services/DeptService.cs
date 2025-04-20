using Backend.Repositories;
using Backend.Models;

namespace Backend.Services
{
    public class DeptService : ServiceBase
    {
        public DeptService(IUnitOfWork unitofwork) : base(unitofwork)
        {

        }

        public List<Dept> GetAll()
        {
            return _unitofwork.Dept.FindAll().ToList();
        }

        public Dept GetSingle(int id)
        {
            //return _unitofwork.Dept.Single(m => m.Id == id);
            var entity = _unitofwork.Dept
                .Include(m => m.Users)
                .SingleOrDefault(m => m.Id == id);

            return entity;
        }

        public void Delete(int id)
        {
            var entity = _unitofwork.Dept.Single(m => m.Id == id);
            if (entity != null)
            {
                _unitofwork.Dept.Delete(entity);
                _unitofwork.Save();
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public void Init()
        {
            var names = new string[] {
                "dept1",
                "dept2",
                "dept3"
                };

            _unitofwork.Dept.DeleteAll();

            for (int i = 0; i < names.Length; i++)
            {
                var item = new Dept();
                item.Id = i + 1;
                item.Name = names[i];
                _unitofwork.Dept.Create(item);
            }

            _unitofwork.Save();
        }

        public void Clear()
        {
            _unitofwork.Dept.DeleteAll();
            _unitofwork.Save();
        }
    }
}