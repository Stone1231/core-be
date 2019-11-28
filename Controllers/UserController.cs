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
using Backend.Repositories;
using Backend.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.Net.Http.Headers;

namespace Backend.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly UserService _service;
        public UserController(UserService service)
        {
            _service = service;
        }

        // GET: api/User
        // [EnableCors]
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _service.GetAll();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public IActionResult GetSingle([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = _service.GetSingle(id);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            user.Id = id;
            // if (id != user.Id)
            // {
            //     return BadRequest();
            // }

            // _context.Entry(user).State = EntityState.Modified;

            _service.Update(user);

            return Ok();
        }

        // POST: api/User
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _service.Create(user);

            return CreatedAtAction("GetSingle", new { id = user.Id }, user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _service.Delete(id);

            return Ok();
        }

        // private bool Exists(int id)
        // {
        //     return _repository.DbSet().Any(e => e.Id == id);
        // }

        [HttpPost("ufile"), DisableRequestSizeLimit]
        public string Upload(IFormFile file)
        {
            if (file != null)
            {
                // // var folder = _hostingEnvironment.WebRootPath + "/img";
                // var folder = Path.Combine("Static", "img");
                // // if (!Directory.Exists(folder))
                // // {
                // //     Directory.CreateDirectory(folder);
                // // }

                // // var filePath = Path.Combine(folder, file.FileName);
                // var filePath = Path.Combine(
                //     Path.Combine(Directory.GetCurrentDirectory(), folder), 
                //     file.FileName);

                // if (System.IO.File.Exists(filePath))
                // {
                //     System.IO.File.Delete(filePath);
                // }

                // using (var fileStream = new FileStream(filePath, FileMode.Create)) {
                //     //await file.CopyToAsync(fileStream);
                //     file.CopyTo(fileStream);
                // }

                // return file.FileName;            
                return FileService.Upload(file, "img");
            }
            return "";
        }

        [HttpPost("query")]
        public IEnumerable<User> Query([FromBody] string name)
        {
            return _service.Query(name);
        }
    }
}