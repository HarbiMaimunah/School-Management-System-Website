using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.IRepository;
using WebApi.Repository;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeacherController : Controller
    {
        private IRepository<Teacher> _teacherRepo;
        
        public TeacherController(IRepository<Teacher> teacherRepo)
        {
            _teacherRepo = teacherRepo;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllTeachers()
        {
            var teachers = _teacherRepo.List();
            if (teachers.Count == 0)
            {
                return NotFound();
            }

            return Ok(teachers);
        }
        
        [HttpGet]
        [Route("Get/{id}")]
        public IActionResult GetTeacherById(int id)
        {
            var teacher = _teacherRepo.Get(id);
            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }

        [HttpPost]
        [Route("Post")]
        public IActionResult PostNewStudent([FromBody] Teacher tch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            _teacherRepo.Add(tch);

            return Ok();
        }

        [HttpPut]
        [Route("Put/{id}")]
        public IActionResult PutExistingTeacher(int id, [FromBody] Teacher tch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            _teacherRepo.Update(tch, id);

            if (_teacherRepo.Update(tch, id) == false)
            {
                return NotFound();
            }

            return Ok();

        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult DeleteTeacher(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Not a valid teacher id");
            }

            _teacherRepo.Delete(id);
            return Ok();
        }
    }
}
