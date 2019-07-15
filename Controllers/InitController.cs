using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Backend.Services;

namespace Backend.Controllers
{
    public class InitController: ControllerBase
    {
        private readonly DeptService _deptService;
        private readonly ProjService _projService;
        private readonly UserService _userService;

        private readonly InitService _initService;

        public InitController(
            DeptService deptService,
            ProjService projService,
            UserService userService,
            InitService initService
            )
        {
            _deptService = deptService;
            _projService = projService;
            _userService = userService;
            _initService = initService;
        }  

        [HttpGet("dept")]
        public IActionResult InitDept()
        {
            _deptService.Init();
            return Ok();
        }

        [HttpDelete("dept")]
        public IActionResult ClearDept()
        {
            _deptService.Clear();
            return Ok();
        }     

        [HttpGet("proj")]
        public IActionResult InitProj()
        {
            _projService.Init();
            return Ok();
        }

        [HttpDelete("proj")]
        public IActionResult ClearProj()
        {
            _projService.Clear();
            return Ok();
        }   
        
        [HttpGet("user")]
        public IActionResult InitUser()
        {
            _userService.Init();
            return Ok();
        }

        [HttpDelete("user")]
        public IActionResult ClearUser()
        {
            _userService.Clear();
            return Ok();
        }

        [HttpGet("all")]
        public IActionResult InitAll()
        {
            _initService.Init();
            return Ok();
        }

        [HttpDelete("all")]
        public IActionResult ClearAll()
        {
            _initService.Clear();
            return Ok();
        }                
    }
}