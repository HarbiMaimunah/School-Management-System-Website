using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.IRepository;


namespace WebApi.Repository
{
    public class TeacherRepo : IRepository<Teacher>
    {
        private SchoolContext _schoolCntx;
        public TeacherRepo(SchoolContext schoolCntx)
        {
            _schoolCntx = schoolCntx;
        }

        public void Add(Teacher item)
        {
            _schoolCntx.Teachers.Add(item);
            _schoolCntx.SaveChanges();
        }

        public void Delete(int id)
        {
            var tch = _schoolCntx.Teachers.Where(t => t.ID == id).FirstOrDefault();
            _schoolCntx.Entry(tch).State = EntityState.Deleted;
            _schoolCntx.SaveChanges();
        }

        public Teacher Get(int id)
        {
            return _schoolCntx.Teachers.Include("Courses").FirstOrDefault(t => t.ID == id);
        }

        public List<Teacher> List()
        {
            return _schoolCntx.Teachers.Include("Courses").ToList();
        }

        public bool Update(Teacher item, int id)
        {
            var tch = _schoolCntx.Teachers.Where(t => t.ID == id).FirstOrDefault<Teacher>();
            if (tch != null)
            {
                tch.FirstName = item.FirstName;
                tch.LastName = item.LastName;
                tch.BirthDate = item.BirthDate;
                tch.Gender = item.Gender;
                tch.Department = item.Department;
                tch.Salary = item.Salary;

                _schoolCntx.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
