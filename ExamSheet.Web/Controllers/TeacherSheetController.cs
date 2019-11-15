using System.Collections.Generic;
using System.Linq;
using ExamSheet.Business.ExamSheet;
using ExamSheet.Business.Faculty;
using ExamSheet.Business.Group;
using ExamSheet.Business.Student;
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

        protected StudentManager StudentManager { get; set; }

        public TeacherSheetController(ExamSheetManager examSheetManager, TeacherManager teacherManager,
            FacultyManager facultyManager, GroupManager groupManager, SubjectManager subjectManager, StudentManager studentManager)
        {
            ExamSheetManager = examSheetManager;
            TeacherManager = teacherManager;
            FacultyManager = facultyManager;
            GroupManager = groupManager;
            SubjectManager = subjectManager;
            StudentManager = studentManager;
        }

        public IActionResult Index()
        {
            var claim = User.Claims.FirstOrDefault(x => x.Type.Equals(Constants.Claims.ReferenceId));
            var sheets = ExamSheetManager.FindAllForTeacher(claim.Value).Select(ExamSheetListViewModel).ToList();
            return View(sheets);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var examSheet = ExamSheetManager.GetById(id);
            //TODO: display exam sheet data
            //TODO: closed date will be changed (if closed)
            //TODO: edit ratings for exam sheet (exam sheet entity will not be updated using SaveOrUpdate)
            var model = CreateViewModel(examSheet);
            return View(model);
        }

        protected virtual TeacherSheetViewModel CreateViewModel(ExamSheetModel model)
        {
            //TODO: Different views for closed and open sheets
            return new TeacherSheetViewModel()
            {
                Id = model.Id,
                CloseDate = model.CloseDate,
                Group = GroupManager.GetById(model.GroupId),
                Faculty = FacultyManager.GetById(model.FacultyId),
                Subject = SubjectManager.GetById(model.SubjectId),
                Teacher = TeacherManager.GetById(model.TeacherId),
                OpenDate = model.OpenDate,
                Semester = model.Semester,
                State = (Models.ExamSheetState)model.State,
                Year = model.Year,
                Ratings = 
            };
        }

        protected virtual IEnumerable<RatingViewModel> CreateRatins(string groupId)
        {
            var students = StudentManager.FindGroup(groupId);
            var res
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