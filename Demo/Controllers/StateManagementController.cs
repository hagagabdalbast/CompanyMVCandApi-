using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class StateManagementController : Controller
    {
        public IActionResult SetTempData()
        {

            return View();
        }
    }
}
