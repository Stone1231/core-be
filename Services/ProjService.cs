using Backend.Repositories;
using Backend.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class ProjService : ServiceBase
    {
        public ProjService(IUnitOfWork unitofwork) : base(unitofwork)
        {

        }

        public List<Proj> GetAll()
        {
            return _unitofwork.Proj.FindAll().ToList();
        }

        public Proj GetSingle(int id)
        {
            var entity = _unitofwork.Proj
                .Include(m => m.UserRelProjs)
                .ThenInclude(m => m.Proj)
                .SingleOrDefault(m => m.Id == id);

            return entity;
        }

        public void Delete(int id)
        {
            var entity = _unitofwork.Proj.Single(m => m.Id == id);
            if (entity != null)
            {
                _unitofwork.Proj.Delete(entity);
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
                "proj1",
                "proj2",
                "proj3"
                };

            _unitofwork.Proj.DeleteAll();

            for (int i = 0; i < names.Length; i++)
            {
                var item = new Proj();
                item.Id = i + 1;
                item.Name = names[i];
                _unitofwork.Proj.Create(item);
            }

            _unitofwork.Save();
        }

        public void Clear()
        {
            _unitofwork.Proj.DeleteAll();
            _unitofwork.Save();
        }
    }
}