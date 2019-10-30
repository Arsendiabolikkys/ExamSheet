using System;
using ExamSheet.Business.ExamSheet;
using ExamSheet.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExamSheet.Web.Controllers
{
    public class ExamSheetController : Controller
    {
        protected ExamSheetManager ExamSheetManager;

        public ExamSheetController(ExamSheetManager examSheetManager)
        {
            this.ExamSheetManager = examSheetManager;
        }

        [HttpGet]
        public IActionResult CreateExamSheet()
        {
            var model = new ExamSheetViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateExamSheet(ExamSheetViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            return RedirectToAction("Index", "Home");
        }
    }
}