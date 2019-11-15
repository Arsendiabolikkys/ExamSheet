using Microsoft.AspNetCore.Mvc;
using ExamSheet.Business.ExamSheet;
using System.Linq;
using ExamSheet.Web.Models;
using System.Diagnostics;
using ExamSheet.Business.Faculty;
using ExamSheet.Business.Group;
using ExamSheet.Business.Subject;
using ExamSheet.Business.Teacher;
using Microsoft.AspNetCore.Authorization;
using ExamSheet.Business.Account;

namespace ExamSheet.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        protected ExamSheetManager ExamSheetManager;

        protected FacultyManager FacultyManager { get; set; }

        protected GroupManager GroupManager { get; set; }

        protected SubjectManager SubjectManager { get; set; }

        protected TeacherManager TeacherManager { get; set; }

        public HomeController(ExamSheetManager examSheetManager, FacultyManager facultyManager, GroupManager groupManager,
            SubjectManager subjectManager, TeacherManager teacherManager)
        {
            this.ExamSheetManager = examSheetManager;
            this.FacultyManager = facultyManager;
            this.GroupManager = groupManager;
            this.SubjectManager = subjectManager;
            this.TeacherManager = teacherManager;
        }

        public IActionResult Index()
        {
            //TODO: Add method to business, use current Role inside method, paging
            //TODO: if deanery - get for deanery, get for teacher if teacher
            if (User.IsInRole(AccountType.Teacher))
                return RedirectToAction("Index", "TeacherSheet");

            var model = CreateIndexPageViewModel();
            return View(model);
        }

        protected virtual IndexPageViewModel CreateIndexPageViewModel()
        {
            var model = new IndexPageViewModel();
            model.ExamSheets = ExamSheetManager.FindAll().Select(ExamSheetListViewModel).ToList();
            return model;
        }

        protected virtual ExamSheetListViewModel ExamSheetListViewModel(ExamSheetModel examSheet)
        {
            var model = new ExamSheetListViewModel();
            model.CloseDate = examSheet.CloseDate;
            model.Faculty = FacultyManager.GetById(examSheet.FacultyId);
            model.Group = GroupManager.GetById(examSheet.GroupId);
            model.Id = examSheet.Id;
            model.OpenDate = examSheet.OpenDate;
            model.Semester = examSheet.Semester;
            model.State = (Web.Models.ExamSheetState)examSheet.State;
            model.Subject = SubjectManager.GetById(examSheet.SubjectId);
            model.Teacher = TeacherManager.GetById(examSheet.TeacherId);
            model.Year = examSheet.Year;
            return model;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
