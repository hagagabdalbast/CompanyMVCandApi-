using Demo.Models;

namespace Demo.Repository
{
    //Crud operation C=Create R=Read U=Update D=Delete
    public class EmployeeRepository:IEmployeeRepository
    {
        ITIEntity context;//= new ITIEntity();
        //inject
        public EmployeeRepository(ITIEntity db)
        {
            context = db;
        }
        public List<Employee> GetAll()
        {
            List<Employee> employees = context.Employees.ToList();
            return employees;
        }
        public Employee GetById(int id) 
        {
            Employee emp = context.Employees.FirstOrDefault(x => x.Id == id);
            return emp;

        }
        public List<Employee> GetByDeptId(int deptid)
        {
            return context.Employees.Where(e => e.Dept_Id == deptid).ToList();
        }
        public void DeleteById(int id) 
        {
            Employee emp = GetById(id);
            context.Employees.Remove(emp);
            context.SaveChanges();
        }
        public void Insert( Employee emp) 
        {

            context.Employees.Add(emp);
            context.SaveChanges();

        }
        public void Update(int id,Employee NewEmp) 
        {
            //old refernce
            Employee oldEmp = GetById(id);
            //new reference
            oldEmp.Name = NewEmp.Name;
            oldEmp.Address = NewEmp.Address;
            oldEmp.Dept_Id = NewEmp.Dept_Id;
            oldEmp.Salary = NewEmp.Salary;
            oldEmp.Image = NewEmp.Image;
            context.SaveChanges();
        }
    }
}
