using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Repository;
using WebApi.IRepository;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : Controller
    {
        private IRepository<Course> _courseRepo;
        public CourseController(IRepository<Course> courseRepo)
        {
            _courseRepo = courseRepo;
        }

        [HttpGet]
        [Route("GetAll")]
        [AllowAnonymous]
        public IActionResult GetAllCourses()
        {
            var courses = _courseRepo.List();
            if (courses.Count == 0)
            {
                return NotFound();
            }

            return Ok(courses);
        }

        [HttpGet]
        [Route("Get/{id}")]
        [AllowAnonymous]
        public IActionResult GetCourseById(int id)
        {
            var course = _courseRepo.Get(id);
            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        [HttpPost]
        [Route("Post")]
        [Authorize]
        public IActionResult PostNewCourse([FromBody] Course crs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            _courseRepo.Add(crs);

            return Ok();
        }

        [HttpPut]
        [Route("Put/{id}")]
        [Authorize]
        public IActionResult PutExistingCourse(int id, [FromBody] Course crs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            _courseRepo.Update(crs, id);

            if (_courseRepo.Update(crs, id) == false)
            {
                return NotFound();
            }

            return Ok();

        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [Authorize]
        public IActionResult DeleteCourse(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Not a valid course id");
            }

            _courseRepo.Delete(id);
            return Ok();
        }
    }
}
