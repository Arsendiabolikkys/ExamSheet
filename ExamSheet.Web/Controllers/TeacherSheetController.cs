using System.Linq;
using ExamSheet.Business.ExamSheet;
using ExamSheet.Business.Faculty;
using ExamSheet.Business.Group;
using ExamSheet.Business.Subject;
using ExamSheet.Business.Teacher;
using ExamSheet.Web.Attributes;
using ExamSheet.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExamSheet.Web.Controllers
{
    [IsInRole(Business.Account.AccountType.Teacher)]
    public class TeacherSheetController : Controller
    {
        protected ExamSheetManager ExamSheetManager { get; set; }

        protected TeacherManager TeacherManager { get; set; }

        protected FacultyManager FacultyManager { get; set; }

        protected GroupManager GroupManager { get; set; }

        protected SubjectManager SubjectManager { get; set; }

        public TeacherSheetController(ExamSheetManager examSheetManager, TeacherManager teacherManager,
            FacultyManager facultyManager, GroupManager groupManager, SubjectManager subjectManager)
        {
            ExamSheetManager = examSheetManager;
            TeacherManager = teacherManager;
            FacultyManager = facultyManager;
            GroupManager = groupManager;
            SubjectManager = subjectManager;
        }

        public IActionResult Index()
        {
            var claim = User.Claims.FirstOrDefault(x => x.Type.Equals(Constants.Claims.ReferenceId));
            var sheets = ExamSheetManager.FindAllForTeacher(claim.Value).Select(ExamSheetListViewModel).ToList();
            return View(sheets);
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
            model.State = (Models.ExamSheetState)examSheet.State;
            model.Subject = SubjectManager.GetById(examSheet.SubjectId);
            model.Teacher = TeacherManager.GetById(examSheet.TeacherId);
            model.Year = examSheet.Year;
            return model;
        }
    }
}