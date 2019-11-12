using Microsoft.AspNetCore.Mvc;
using ExamSheet.Business.ExamSheet;
using System.Linq;
using ExamSheet.Web.Models;
using System.Diagnostics;

namespace ExamSheet.Web.Controllers
{
    public class HomeController : Controller
    {
        protected ExamSheetManager ExamSheetManager;

        public HomeController(ExamSheetManager examSheetManager)
        {
            this.ExamSheetManager = examSheetManager;
        }

        public IActionResult Index()
        {
            //TODO: Add method to business, use current Role inside method, paging
            var model = CreateIndexPageViewModel();
            return View(model);
        }

        protected virtual IndexPageViewModel CreateIndexPageViewModel()
        {
            var model = new IndexPageViewModel();
            model.ExamSheets = ExamSheetManager.FindAll().ToList();
            return model;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
