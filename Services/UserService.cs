using Backend.Repositories;
using Backend.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class UserService : ServiceBase
    {
        public UserService(IUnitOfWork unitofwork) : base(unitofwork)
        {

        }

        public List<User> GetAll()
        {
            return _unitofwork.User.FindAll().ToList();
        }

        public User GetSingle(int id)
        {
            var entity = _unitofwork.User
                .Include(m => m.Dept)
                .Include(m => m.UserRelProjs)
                .ThenInclude(m => m.Proj)
                .SingleOrDefault(m => m.Id == id);

            entity.Projs = entity.UserRelProjs.Select(
                m => m.ProjId
            ).ToList();

            return entity;
        }

        public void Create(User entity)
        {
            // var dept = _unitofwork.Dept.Single(m=>m.Id==entity.DeptId);
            // entity.Dept = dept;

            var projs = _unitofwork.Proj
            .FindByCondition(m => entity.Projs.Contains(m.Id), true);

            entity.UserRelProjs = new List<UserRelProj>();

            foreach (var proj in projs)
            {
                var mapping = new UserRelProj();
                // mapping.UserId = item.Id;
                // mapping.ProjId = proj.Id;                    
                mapping.User = entity; //entity ok too
                mapping.Proj = proj;
                //entity.UserRelProjs.Add(mapping); unsuccess!
                entity.UserRelProjs.Add(mapping);
            }

            _unitofwork.User.Create(entity);
            _unitofwork.Save();
        }

        public void Update(User entity)
        {
            // var ori = _unitofwork.User.Single(m => m.Id == entity.Id);
            var ori = _unitofwork.User
                .Include(m => m.Dept)
                .Include(m => m.UserRelProjs)
                .ThenInclude(m => m.Proj)
                .SingleOrDefault(m => m.Id == entity.Id);

            // entity.UserRelProjs = new List<UserRelProj>();

            ori.UserRelProjs.Clear();

            var projs = _unitofwork.Proj
            .FindByCondition(m => entity.Projs.Contains(m.Id));
            foreach (var proj in projs)
            {
                var mapping = new UserRelProj();
                // mapping.UserId = item.Id;
                // mapping.ProjId = proj.Id;                    
                mapping.User = ori; //entity ok too
                mapping.Proj = proj;
                //entity.UserRelProjs.Add(mapping); unsuccess!
                ori.UserRelProjs.Add(mapping);
            }

            ////Error Null TypeMapping in Sql Tree ...
            // var mappings = _unitofwork.Proj
            // .FindByCondition(m => entity.Projs.Contains(m.Id))
            // .Select(m => new UserRelProj
            // {
            //     UserId = entity.Id,
            //     ProjId = m.Id,
            //     User = ori,
            //     Proj = m
            // });
            // ori.UserRelProjs = mappings.ToList();

            _unitofwork.User.Entry(ori).CurrentValues.SetValues(entity);
            _unitofwork.Save();
        }

        public void Delete(int id)
        {
            var entity = _unitofwork.User.Single(m => m.Id == id);
            if (entity != null)
            {
                _unitofwork.User.Delete(entity);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public void Init()
        {
            _unitofwork.User.DeleteAll();

            var item = new User();
            item.Name = "user1";
            item.Hight = 170;
            item.Photo = "";
            item.Birthday = new DateTime(1977, 12, 31, 0, 0, 0);

            var dept = _unitofwork.Dept.FindAll(true).First();
            item.Dept = dept;

            item.UserRelProjs = new List<UserRelProj>();
            var projs = _unitofwork.Proj.FindAll(true);
            foreach (var proj in projs)
            {
                var mapping = new UserRelProj();
                // mapping.UserId = item.Id;
                // mapping.ProjId = proj.Id;                    
                mapping.User = item;
                mapping.Proj = proj;
                item.UserRelProjs.Add(mapping);
            }

            _unitofwork.User.Create(item);
            _unitofwork.Save();
        }

        public void Clear()
        {
            _unitofwork.User.DeleteAll();
            _unitofwork.Save();
        }

        public List<User> Query(string name)
        {
            return _unitofwork.User.FindByCondition(m=>m.Name.Contains(name)).ToList();
        }
    }
}