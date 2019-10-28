using Microsoft.AspNetCore.Mvc;
using ExamSheet.Business.ExamSheet;
using System.Linq;

namespace ExamSheet.Web.Controllers
{
    public class HomeController : Controller
    {
        protected ExamSheetManager examSheetManager;

        public HomeController(ExamSheetManager examSheetManager)
        {
            //TODO: check if initialized
            this.examSheetManager = examSheetManager;
        }

        public IActionResult Index()
        {
            //TODO: Add method to business, use current Role inside method, paging
            var examSheets = examSheetManager.FindAll().ToList();
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
