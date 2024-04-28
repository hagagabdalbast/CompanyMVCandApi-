using Demo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class BindController : Controller
    {
        public IActionResult testPrimitive(int id,string name,int age)
        {

            return Content($"name={name} ,id={id} ,age={age}");
        }

        public IActionResult testDic(Dictionary<string,int> phones,string name)
        {

            return Content($"name={name}");
        }
        //    public IActionResult testComplex(int od,string name,string mangername,Department dept)
        //    {
        //        return Content("OK");
        //    }

        public IActionResult testComplex([Bind(include:"Id,Name")] Department dept)
        {
            return Content("OK");
        }
    }

}
