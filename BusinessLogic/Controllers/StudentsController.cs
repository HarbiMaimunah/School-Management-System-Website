using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.IRepository;

namespace WebApi.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private IRepository<Student> _studentRepo;
        private readonly SchoolContext context;
        public StudentsController(IRepository<Student> studentRepo, SchoolContext context)
        {
            this._studentRepo = studentRepo;
            this.context = context;
        }

        public IActionResult Index()
        {
            var students = _studentRepo.List();
            return View(students);
        }

        [HttpPost]
        public IActionResult GetStudents()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault(); //The Number of times the API is called for the current datatable.
                var start = Request.Form["start"].FirstOrDefault(); //The count of records to skip. This will be used while Paging in EFCore
                var length = Request.Form["length"].FirstOrDefault(); //Essentially the page size. You can see the Top Dropdown in the Jquery Datatable that says, ‘Showing n entries’.
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][firstName]"].FirstOrDefault(); //The Column that is set for sorting
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault(); //Ascending / Descending
                var searchValue = Request.Form["search[value]"].FirstOrDefault(); //The Value from the Search Box
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var studentData = (from tempstudent in context.Students select tempstudent); //Gets an IQueryable of the DataSource
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection))) //Sorting
                {
                    studentData = studentData.OrderBy(x=>x.FirstName + " " + sortColumnDirection);
                } //Sorting
                if (!string.IsNullOrEmpty(searchValue)) //Searching. Here we will search through each column.
                {
                    studentData = studentData.Where(s => s.FirstName.Contains(searchValue)
                                                || s.LastName.Contains(searchValue)
                                                || s.BirthDate.ToString().Contains(searchValue)
                                                || s.Gender.Contains(searchValue)
                                                || s.EnrollmentDate.ToString().Contains(searchValue)
                                                || s.Major.Contains(searchValue)
                                                || s.Status.Contains(searchValue)
                                                || s.GPA.ToString().Contains(searchValue));
                } //Searching. Here we will search through each column.
                recordsTotal = studentData.Count(); //Gets the total count of the Records.
                var data = studentData.Skip(skip).Take(pageSize).ToList(); //Performs paging using EFCore
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }; //Sets the data in the required format and returns it.
                return Ok(jsonData); //Sets the data in the required format and returns it.

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student stu)
        {
            if(stu.FirstName == "" || stu.LastName == "" || stu.Gender == "" || stu.GPA < 2 || stu.GPA > 5)
            {
                return RedirectToAction("Create");
            }
            else
            {
                _studentRepo.Add(stu);
                return RedirectToAction("Index");
            }
        }

        public IActionResult Edit(int id)
        {
            Student stu = _studentRepo.Get(id);
            return View(stu);
        }

        [HttpPost]
        public IActionResult Update(Student stu)
        {
            if (stu.FirstName == "" || stu.LastName == "" || stu.Gender == "" || stu.GPA < 2 || stu.GPA > 5)
            {
                return RedirectToAction("Edit");
            }
            else
            {
                _studentRepo.Update(stu, stu.ID);
                return RedirectToAction("Index");
            }
        }

        public IActionResult Delete(int id)
        {
            _studentRepo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
