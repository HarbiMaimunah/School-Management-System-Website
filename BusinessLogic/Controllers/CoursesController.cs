using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.IRepository;


namespace WebApi.Controllers
{
    public class CoursesController : Controller
    {
        private IRepository<Course> _courseRepo;
        private readonly SchoolContext context;
        public CoursesController(IRepository<Course> courseRepo, SchoolContext context)
        {
            this._courseRepo = courseRepo;
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult GetCourses()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault(); //The Number of times the API is called for the current datatable.
                var start = Request.Form["start"].FirstOrDefault(); //The count of records to skip. This will be used while Paging in EFCore
                var length = Request.Form["length"].FirstOrDefault(); //The page size. See the Top Dropdown in the Jquery Datatable that says, ‘Showing n entries’.
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault(); //The Column that is set for sorting
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault(); //Ascending / Descending
                var searchValue = Request.Form["search[value]"].FirstOrDefault(); //The Value from the Search Box
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var courseData = (from tempcourse in context.Courses select tempcourse); //Gets an IQueryable of the DataSource
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection))) //Sorting
                {
                    courseData = courseData.OrderBy(sortColumn + " " + sortColumnDirection);
                } //Sorting
                if (!string.IsNullOrEmpty(searchValue)) //Searching. Here we will search through each column.
                {
                    courseData = courseData.Where(c => c.Name.Contains(searchValue)
                                                || c.CreditHours.ToString().Contains(searchValue));
                } //Searching. Here we will search through each column.
                recordsTotal = courseData.Count(); //Gets the total count of the Records.
                var data = courseData.Skip(skip).Take(pageSize).ToList(); //Performs paging using EFCore
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }; //Sets the data in the required format and returns it.
                return Ok(jsonData); //Sets the data in the required format and returns it.

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(Course crs)
        {
            if(crs.Name == "" || crs.CreditHours < 2 || crs.CreditHours > 7)
            {
                return RedirectToAction("Create");
            }
            else
            {
                _courseRepo.Add(crs);
                return RedirectToAction("Index");
            }
        }
        
        [Authorize]
        public IActionResult Edit(int id)
        {
            Course crs = _courseRepo.Get(id);
            return View(crs);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Update(Course crs)
        {
            if (crs.Name == "" || crs.CreditHours < 2 || crs.CreditHours > 7)
            {
                return RedirectToAction("Edit");
            }
            else{
                _courseRepo.Update(crs, crs.ID);
                return RedirectToAction("Index");
            }
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            _courseRepo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
