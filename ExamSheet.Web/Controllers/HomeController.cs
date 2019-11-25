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
using ExamSheet.Business.Deanery;

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

        protected DeaneryManager DeaneryManager { get; set; }

        protected int PageSize { get; set; } = 8;

        public HomeController(ExamSheetManager examSheetManager, FacultyManager facultyManager, GroupManager groupManager,
            SubjectManager subjectManager, TeacherManager teacherManager, DeaneryManager deaneryManager)
        {
            this.ExamSheetManager = examSheetManager;
            this.FacultyManager = facultyManager;
            this.GroupManager = groupManager;
            this.SubjectManager = subjectManager;
            this.TeacherManager = teacherManager;
            this.DeaneryManager = deaneryManager;
        }

        public IActionResult Index(int page = 1)
        {
            if (User.IsInRole(AccountType.Admin))
                return RedirectToAction("Index", "Faculty");
            if (User.IsInRole(AccountType.Teacher))
                return RedirectToAction("Index", "TeacherSheet");

            var model = CreateIndexPageViewModel(page);
            return View(model);
        }

        protected virtual IndexPageViewModel CreateIndexPageViewModel(int page)
        {
            var model = new IndexPageViewModel();
            var claim = User.Claims.FirstOrDefault(x => x.Type.Equals(Constants.Claims.ReferenceId));
            var deanery = DeaneryManager.GetById(claim.Value);
            var totalCount = ExamSheetManager.GetTotalForFaculty(deanery.FacultyId);
            model.Page = new PageViewModel(totalCount, page, PageSize);
            model.ExamSheets = ExamSheetManager.FindAllForFaculty(deanery.FacultyId, page, PageSize).Select(ExamSheetListViewModel).ToList();
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
