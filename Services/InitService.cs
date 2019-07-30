using Backend.Repositories;
using Backend.Models;
using System;

namespace Backend.Services
{
    public class InitService : ServiceBase
    {
        private readonly DeptService _deptService;
        private readonly ProjService _projService;
        private readonly UserService _userService;
        public InitService(
            IUnitOfWork unitofwork,
            DeptService deptService,
            ProjService projService,
            UserService userService
            ) : base(unitofwork)
        {
            _deptService = deptService;
            _projService = projService;
            _userService = userService;
        }

        public void Init()
        {
            using (var transaction = _unitofwork.Transaction())
            {
                _deptService.Init();
                _projService.Init();
                _userService.Init();
                FileService.Clear("img");
                transaction.Commit();
            }
        }

        public void Clear()
        {
            using (var transaction = _unitofwork.Transaction())
            {
                _deptService.Clear();
                _projService.Clear();
                _userService.Clear();
                FileService.Clear("img");
                transaction.Commit();
            }
        }
    }
}