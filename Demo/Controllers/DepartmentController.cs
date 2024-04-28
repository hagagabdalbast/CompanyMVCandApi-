using Demo.Models;
using Demo.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;

namespace Demo.Controllers
{
    public class DepartmentController : Controller
    {
        //ITIEntity context = new ITIEntity();

        IDepartmentRepository departmentRepository;//= new DepartmentRepository();
        IEmployeeRepository employeeRepository;//= new EmployeeRepository();

        //implement Dependency Injection
        public DepartmentController(IEmployeeRepository EmpRepo, IDepartmentRepository DeptRepo)
        {
            //inject
            departmentRepository = DeptRepo;
            employeeRepository = EmpRepo;

        }
        public IActionResult ShowEmployeeDepartment()
        {
            List<Department> deptlist = departmentRepository.GetAll();//context.Departments.ToList();
            return View(deptlist);
        }
        public IActionResult ShowEmployeePerDepartment(int deptId)
        {
            List<Employee> emps = employeeRepository.GetByDeptId(deptId);//context.Employees.Where(e=>e.Dept_Id==deptId).ToList();
            return Json(emps);
        }
        public IActionResult Index()
        {
            //get all department

            List<Department> deptlistModel = departmentRepository.GetAll();//context.Departments.ToList();

            return View("Index", deptlistModel);
        }
        //anchor tag to open empty form
        [HttpGet]
        public IActionResult New()
        {
            return View(new Department());
        }

        //submit button to save data
        [HttpPost]
        public IActionResult SaveNew(Department dept)
        {
            if (dept.Name != null)
            {
                //context.Departments.Add(dept);
                //context.SaveChanges();
                departmentRepository.Insert(dept);
                return RedirectToAction("Index");
            }
            else
            {
                return View("New",dept);
            }
          
           
        }
    }
}
