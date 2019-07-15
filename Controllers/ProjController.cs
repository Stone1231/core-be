using System.Collections.Generic;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class ProjController : ControllerBase
    {
        private readonly ProjService _service;
        public ProjController(
            ProjService service)
        {
            _service = service;
        }  
        
        [HttpGet]
        public IEnumerable<Proj> Get()
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
    }
}