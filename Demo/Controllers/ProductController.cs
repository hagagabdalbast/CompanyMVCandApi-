﻿using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
