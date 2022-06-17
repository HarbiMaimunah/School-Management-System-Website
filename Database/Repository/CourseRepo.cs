using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.IRepository;

namespace WebApi.Repository
{
    public class CourseRepo : IRepository<Course>
    {
        private SchoolContext _schoolCntx;
        public CourseRepo(SchoolContext schoolCntx)
        {
            _schoolCntx = schoolCntx;
        }
        
        public void Add(Course item)
        {
            _schoolCntx.Courses.Add(item);
            _schoolCntx.SaveChanges();
        }

        public void Delete(int id)
        {
            var crs = _schoolCntx.Courses.Where(c => c.ID == id).FirstOrDefault();
            _schoolCntx.Entry(crs).State = EntityState.Deleted;
            _schoolCntx.SaveChanges();
        }

        public Course Get(int id)
        {
            return _schoolCntx.Courses.Include("Students").Include("Teachers").FirstOrDefault(c => c.ID == id);
        }

        public List<Course> List()
        {
            return _schoolCntx.Courses.Include("Students").Include("Teachers").ToList();
        }

        public bool Update(Course item, int id)
        {
            var crs = _schoolCntx.Courses.Where(c => c.ID == id).FirstOrDefault<Course>();
            if (crs != null)
            {
                crs.Name = item.Name;
                crs.CreditHours = item.CreditHours;

                _schoolCntx.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
