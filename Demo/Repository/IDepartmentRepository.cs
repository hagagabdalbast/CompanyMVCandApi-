using Demo.Models;

namespace Demo.Repository
{
    public interface IDepartmentRepository
    {
        List<Department> GetAll();
        
        Department GetById(int id);
        
        List<Department> GetAllDepartmentWithEmployeeName();
        
        void Insert(Department dept);
       
        void Update(int id, Department newDept);
       
        void DeleteById(int id);
     
    }
}
