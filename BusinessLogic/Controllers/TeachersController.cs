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


namespace WebApi.Controllers
{
    [Authorize]
    public class TeachersController : Controller
    {
        private IRepository<Teacher> _teacherRepo;
        private readonly SchoolContext context;
        public TeachersController(IRepository<Teacher> _teacherRepo, SchoolContext context)
        {
            this._teacherRepo = _teacherRepo;
            this.context = context;
        }
        
        public IActionResult Index()
        {
            return View();
            
        }

        [HttpPost]
        public IActionResult GetTeachers()
        {

            try
            {
                var draw = Request.Form["draw"].FirstOrDefault(); //The Number of times the API is called for the current datatable.
                var start = Request.Form["start"].FirstOrDefault(); //The count of records to skip. This will be used while Paging in EFCore
                var length = Request.Form["length"].FirstOrDefault(); //Essentially the page size. You can see the Top Dropdown in the Jquery Datatable that says, ‘Showing n entries’. n is the page size.
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault(); //The Column that is set for sorting
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault(); //Ascending / Descending
                var searchValue = Request.Form["search[value]"].FirstOrDefault(); //The Value from the Search Box
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var teacherData = (from tempteacher in context.Teachers select tempteacher); //Gets an IQueryable of the DataSource
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection))) //Sorting
                {
                    teacherData = teacherData.OrderBy(sortColumn + " " + sortColumnDirection);
                } //Sorting
                if (!string.IsNullOrEmpty(searchValue)) //Searching. Here we will search through each column.
                {
                    teacherData = teacherData.Where(t => t.FirstName.Contains(searchValue)
                                                || t.LastName.Contains(searchValue)
                                                || t.BirthDate.ToString().Contains(searchValue)
                                                || t.Gender.Contains(searchValue)
                                                || t.Department.Contains(searchValue)
                                                || t.Salary.ToString().Contains(searchValue));
                } //Searching. Here we will search through each column.
                recordsTotal = teacherData.Count(); //Gets the total count of the Records. According to me, this is the most expensive query in this entire controller code. You could probably avoid this by other means like storing the total records somewhere in another table, maybe?
                var data = teacherData.Skip(skip).Take(pageSize).ToList(); //Performs paging using EFCore
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
        public IActionResult Create(Teacher tch)
        {
            if(tch.FirstName == "" || tch.LastName == "" || tch.Gender == "" || tch.Salary < 5000 || tch.Salary > 15000)
            {
                return RedirectToAction("Create");
            }
            else
            {
                _teacherRepo.Add(tch);
                return RedirectToAction("Index");
            }
        }

        public IActionResult Edit(int id)
        {
            Teacher tch = _teacherRepo.Get(id);
            return View(tch);
        }

        [HttpPost]
        public IActionResult Update(Teacher tch)
        {
            if (tch.FirstName == "" || tch.LastName == "" || tch.Gender == "" || tch.Salary < 5000 || tch.Salary > 15000)
            {
                return RedirectToAction("Edit");
            }
            else
            {
                _teacherRepo.Update(tch, tch.ID);
                return RedirectToAction("Index");
            }
        }

        public IActionResult Delete(int id)
        {
            _teacherRepo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
