using Demo.Filters;
using Demo.Models;
using Demo.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class EmployeeController : Controller
    {
        //ITIEntity context = new ITIEntity();
        IDepartmentRepository departmentRepository; //= new DepartmentRepository();
        IEmployeeRepository employeeRepository;//= new EmployeeRepository();
        //Dont create , ask
        //implement Dependency Injection
        public EmployeeController(IEmployeeRepository EmpRepo, IDepartmentRepository DeptRepo)
        {
            //inject
            employeeRepository = EmpRepo;
            departmentRepository = DeptRepo;
        }

        // [MyFilter] //custom filter
         [Authorize] //built in filter and Filter Authentication in main program to run
        //[AllowAnonymous] //built in filter for dont make this action to Authorize
        public IActionResult Index()
        {
            return View(employeeRepository.GetAll());//context.Departments.Include(d=>d.employees).ToList());
        }

        //anchor tag
       // [ResponseCache(Duration =,Location =)] //built in filter
        public IActionResult Edit(int id)
        {
            Employee empModel = employeeRepository.GetById(id);//context.Employees.FirstOrDefault(e => e.Id == id);

            ViewData["deptList"] = departmentRepository.GetAll(); //context.Departments.ToList();

            return View(empModel);
        }

        //submit to save database
        [HttpPost]
        public IActionResult SaveEdit(int id,Employee newEmployee)
        {
            Employee oldEmployee = employeeRepository.GetById(id);//context.Employees.FirstOrDefault(e => e.Id == id);

            if (oldEmployee != null)
            {
                //    oldEmployee.Name = newEmployee.Name;
                //    oldEmployee.Salary = newEmployee.Salary;
                //    oldEmployee.Image = newEmployee.Image;
                //    oldEmployee.Dept_Id = newEmployee.Dept_Id;
                //    oldEmployee.Address = newEmployee.Address;

                //    context.SaveChanges();

                employeeRepository.Update(id, newEmployee);

                return RedirectToAction("Index");
            }
            else
            {

                ViewData["deptList"] = departmentRepository.GetAll();//context.Departments.ToList();
                return View("Edit", newEmployee);
            }



        }

        public IActionResult New()
        {
            ViewData["DeptDropDownList"] = departmentRepository.GetAll();//context.Departments.ToList();
            return View();
        }
        [HttpPost]
        //call internal
        [ValidateAntiForgeryToken] //used in tag helper or html helper
        public IActionResult SaveNew(Employee newEmployee)
        {
            //any validtion
            if (ModelState.IsValid)
            {
                //context.Employees.Add(newEmployee);
                //context.SaveChanges();
                employeeRepository.Insert(newEmployee);
                return RedirectToAction("Index");

            }
            else
            {
                ViewData["DeptDropDownList"] = departmentRepository.GetAll();
                return View("New",newEmployee);
            }
           
        }
        //the name of paramter must be the same of peroperty 
        public IActionResult CheckSalary(int Salary)
        {
            if (Salary > 2000)
            {
                return Json(true);
            }
            return Json(false);
        }

        public IActionResult Details(int id)
        {
            return View(employeeRepository.GetById(id));//context.Employees.FirstOrDefault(e => e.Id == id));
        }
        public IActionResult DetailsUsingPartialView(int id)
        {
            Employee emp = employeeRepository.GetById(id); //context.Employees.FirstOrDefault(e => e.Id == id);
            return PartialView("_EmployeeCardPartial",emp);
        }
    }
}
