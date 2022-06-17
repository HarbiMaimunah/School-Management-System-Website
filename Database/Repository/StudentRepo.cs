using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.IRepository;


namespace WebApi.Repository
{
    public class StudentRepo : IRepository<Student>
    {
        private SchoolContext _schoolCntx;
        public StudentRepo(SchoolContext schoolCntx)
        {
            _schoolCntx = schoolCntx;
        }
   
        public void Add(Student item)
        {
            _schoolCntx.Students.Add(item);
            _schoolCntx.SaveChanges();
        }

        public void Delete(int id)
        {
            var stu = _schoolCntx.Students.Where(s => s.ID == id).FirstOrDefault();
            _schoolCntx.Entry(stu).State = EntityState.Deleted;
            _schoolCntx.SaveChanges();
        }

        public Student Get(int id)
        {
            return _schoolCntx.Students.Include("Courses").FirstOrDefault(s => s.ID == id);
        }

        public List<Student> List()
        {
            return _schoolCntx.Students.Include("Courses").ToList();
        }

        public bool Update(Student item, int id)
        {
            var stu = _schoolCntx.Students.Where(s => s.ID == id).FirstOrDefault<Student>();
            if (stu != null)
            {
                stu.FirstName = item.FirstName;
                stu.LastName = item.LastName;
                stu.BirthDate = item.BirthDate;
                stu.Gender = item.Gender;
                stu.EnrollmentDate = item.EnrollmentDate;
                stu.Major = item.Major;
                stu.Status = item.Status;
                stu.GPA = item.GPA;

                _schoolCntx.SaveChanges();

                return true;
            }

            return false;

        }
    }
}
