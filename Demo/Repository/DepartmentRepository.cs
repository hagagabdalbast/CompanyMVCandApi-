using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Repository
{
    public class DepartmentRepository:IDepartmentRepository
    {
        ITIEntity context;//= new ITIEntity();
        //inject
        public DepartmentRepository(ITIEntity db)
        {
            context = db;
        }

        public List<Department> GetAll()
        {
            return context.Departments.ToList();
        }
        public Department GetById(int id)
        {
            return context.Departments.FirstOrDefault(x=>x.Id==id);
        }
        public List<Department> GetAllDepartmentWithEmployeeName()
        {
            return context.Departments.Include(d => d.Employees).ToList();
        }
        public void Insert(Department dept)
        {
            context.Departments.Add(dept);
            context.SaveChanges();
        }
        public void Update(int id,Department newDept)
        {
            //oldrefernce
            Department oldDept = GetById(id);
            //newrefernce
            oldDept.Name = newDept.Name;
            oldDept.MangerName = newDept.MangerName;
            //savechanges
            context.SaveChanges();
        }
        public void DeleteById(int id)
        {
            Department dept = GetById(id);
            context.Departments.Remove(dept);
            context.SaveChanges();

        }
    }
}
