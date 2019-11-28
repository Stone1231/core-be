using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Services;

namespace Backend.Controllers
{
    public class DeptController : ControllerBase
    {
        private readonly DeptService _service;
        public DeptController(
            DeptService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Dept> Get()
        {
            return _service.GetAll();
        }

        [HttpGet("{id}")]
        //public async Task<IActionResult> GetSingle([FromRoute] int id)
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
    }
}