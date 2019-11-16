using ExamSheet.Business.Account;
using ExamSheet.Business.ExamSheet;
using ExamSheet.Business.Rating;
using ExamSheet.Web.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ExamSheet.Web.Controllers
{
    [Authorize]
    public class ChartController : Controller
    {
        protected ExamSheetManager ExamSheetManager { get; set; }

        protected RatingManager RatingManager { get; set; }

        public ChartController(ExamSheetManager examSheetManager, RatingManager ratingManager)
        {
            ExamSheetManager = examSheetManager;
            RatingManager = ratingManager;
        }
        
        public IActionResult Index()
        {
            if (User.IsInRole(AccountType.Deanery))
                return DeaneryIndex();
            else if (User.IsInRole(AccountType.Teacher))
                return TeacherIndex();

            return RedirectToAction("Index", "Home");
        }

        [IsInRole(AccountType.Teacher)]
        public IActionResult TeacherIndex()
        {
            var referenceId = User.Claims.FirstOrDefault(x => x.Type.Equals(Constants.Claims.ReferenceId));
            var sheets = ExamSheetManager.FindClosedForTeacher(referenceId.Value);
            //TODO: create A, B, C, D, E, F ratings bar chart
            //separate js files for teacher and deanery
            //TODO: think how visualise chart data on page (change groups etc)
            return View();
        }

        [IsInRole(AccountType.Deanery)]
        public IActionResult DeaneryIndex()
        {
            var referenceId = User.Claims.FirstOrDefault(x => x.Type.Equals(Constants.Claims.ReferenceId));
            return View();
        }
    }
}