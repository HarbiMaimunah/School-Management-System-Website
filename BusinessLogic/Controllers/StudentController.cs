using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using WebApi.IRepository;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : Controller
    {
        private IRepository<Student> _studentRepo;
        public StudentController(IRepository<Student> studentRepo)
        {
            _studentRepo = studentRepo;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllStudents()
        {
            var students = _studentRepo.List();
            if (students.Count == 0)
            {
                return NotFound();
            }

            return Ok(students);
        }

        [HttpGet]
        [Route("Get/{id}")]
        public IActionResult GetStudentById(int id)
        {
            var student = _studentRepo.Get(id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPost]
        [Route("Post")]
        public IActionResult PostNewStudent([FromBody] Student stu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            _studentRepo.Add(stu);
            return Ok();
        }

        [HttpPut]
        [Route("Put/{id}")]
        public IActionResult PutExistingStudent(int id, [FromBody] Student stu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            _studentRepo.Update(stu, id);

            if (_studentRepo.Update(stu, id) == false)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult DeleteStudent(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Not a valid student id");
            }

            _studentRepo.Delete(id);
            return Ok();
        }
    }
}

