using Demo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class PassDataController : Controller
    {
        ITIEntity context = new ITIEntity();
        public IActionResult TestViewData()
        {
            Employee EmpModel = context.Employees.FirstOrDefault();

            string msg = "Hello";

            List<string> branches = new List<string>();

            branches.Add ("Alex");
            branches.Add ("Minia");
            branches.Add ("Assuit");
            branches.Add ("Sohag");

            int temp = 44;

            ViewData["Message"] = msg;
            ViewData["Branch"] = branches;
            ViewData["Temp"] = temp;

            return View(EmpModel);
        }
    }
}
