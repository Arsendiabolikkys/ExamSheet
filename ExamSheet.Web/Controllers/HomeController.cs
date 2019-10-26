using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExamSheet.Web.Models;
using ExamSheet.Business.ExamSheet;

namespace ExamSheet.Web.Controllers
{
    public class HomeController : Controller
    {
        protected IExamSheetManager examSheetManager;

        public HomeController(IExamSheetManager examSheetManager)
        {
            //TODO: check if initialized
            this.examSheetManager = examSheetManager;
        }

        public IActionResult Index()
        {
            //TODO: Add method to business, use current Role inside method, paging
            var examSheets = examSheetManager.GetExamSheets();
            return View();
        }

        //public IActionResult About()
        //{
        //    ViewData["Message"] = "Your application description page.";

        //    return View();
        //}

        //public IActionResult Contact()
        //{
        //    ViewData["Message"] = "Your contact page.";

        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
