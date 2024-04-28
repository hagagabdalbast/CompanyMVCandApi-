using Demo.Models;

namespace Demo.Repository
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAll();
        
        Employee GetById(int id);
      
        List<Employee> GetByDeptId(int deptid);
      
        void DeleteById(int id);
    
        void Insert(Employee emp);
        void Update(int id, Employee NewEmp);
 
    }
}
