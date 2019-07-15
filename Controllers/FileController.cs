using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class FileController : ControllerBase{

        [HttpPost("ufiles")]
        public IActionResult Uploads(List<IFormFile> files)
        {
            var list= new List<String>();

            foreach (var file in files)
            {
                list.Add(file.FileName);
                // to do save
            }

            return Ok(list);
        } 

        [HttpPost("ufile")]
        public IActionResult Upload(IFormFile file)
        {
           var _file = file; 

            return Ok(file.FileName);
        } 

        [HttpPost("ufile2")]
        public IActionResult Upload2(IFormFile file1, IFormFile file2)
        {
            var list = new List<String>();
            list.Add(file1.FileName);
            list.Add(file2.FileName);

            return Ok(list);
        }
    }
}

