using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ExamSheet.Web.Controllers
{
    public class DeaneryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}