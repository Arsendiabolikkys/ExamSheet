using System.Collections.Generic;
using System.Linq;
using ExamSheet.Business.ExamSheet;
using ExamSheet.Business.Faculty;
using ExamSheet.Business.Group;
using ExamSheet.Business.Rating;
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

        protected RatingManager RatingManager { get; set; }

        protected int PageSize { get; set; } = 9;

        public TeacherSheetController(ExamSheetManager examSheetManager, TeacherManager teacherManager, FacultyManager facultyManager, 
            GroupManager groupManager, SubjectManager subjectManager, StudentManager studentManager, RatingManager ratingManager)
        {
            ExamSheetManager = examSheetManager;
            TeacherManager = teacherManager;
            FacultyManager = facultyManager;
            GroupManager = groupManager;
            SubjectManager = subjectManager;
            StudentManager = studentManager;
            RatingManager = ratingManager;
        }

        public IActionResult Index(int page = 1)
        {
            //TODO: edit only open sheets
            var model = CreateIndexPageViewModel(page);
            return View(model);
        }

        protected virtual IndexPageViewModel CreateIndexPageViewModel(int page)
        {
            var model = new IndexPageViewModel();
            var claim = User.Claims.FirstOrDefault(x => x.Type.Equals(Constants.Claims.ReferenceId));
            var totalCount = ExamSheetManager.GetTotalForTeacher(claim.Value);
            model.Page = new PageViewModel(totalCount, page, PageSize);
            model.ExamSheets = ExamSheetManager.FindAllForTeacher(claim.Value, page, PageSize).Select(ExamSheetListViewModel).ToList();
            return model;
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var examSheet = ExamSheetManager.GetById(id);
            var model = CreateViewModel(examSheet);
            return View(model);
        }

        [HttpGet]
        public IActionResult ViewSheet(string id)
        {
            var examSheet = ExamSheetManager.GetById(id);
            var model = CreateViewModel(examSheet);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TeacherSheetViewModel model, string save, string saveAndClose)
        {
            bool shouldClose = !string.IsNullOrEmpty(saveAndClose);
            var valid = ModelState.IsValid && (shouldClose && ExamSheetManager.CloseSheet(model.Id) || !shouldClose);
            if (!valid)
            {
                var examSheet = ExamSheetManager.GetById(model.Id);
                return View("Edit", CreateViewModel(examSheet));
            }
            
            RatingManager.SaveRatings(model.Ratings.Select(CreateRatingModel));
            return RedirectToAction("Index");
        }

        protected virtual RatingModel CreateRatingModel(RatingViewModel ratingViewModel)
        {
            return new RatingModel()
            {
                ExamSheetId = ratingViewModel.ExamSheetId,
                Mark = ratingViewModel.Mark,
                StudentId = ratingViewModel.StudentId
            };
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
                Ratings = CreateRatings(model.GroupId, model.Id)
            };
        }

        protected virtual List<RatingViewModel> CreateRatings(string groupId, string examSheetId)
        {
            var ratings = RatingManager.FindAll(examSheetId);
            if (ratings?.Any() ?? false)
                return ratings.Select(CreateRatingViewModel).OrderBy(x => x.Student.Surname).ToList();

            var students = StudentManager.FindGroup(groupId);
            return students.Select(student => CreateRatingViewModel(student, examSheetId)).OrderBy(x => x.Student.Surname).ToList();
        }

        protected virtual RatingViewModel CreateRatingViewModel(RatingModel rating)
        {
            return new RatingViewModel()
            {
                ExamSheetId = rating.ExamSheetId,
                Mark = rating.Mark,
                Student = StudentManager.GetById(rating.StudentId),
                StudentId = rating.StudentId
            };
        }

        protected virtual RatingViewModel CreateRatingViewModel(StudentModel student, string examSheetId)
        {
            return new RatingViewModel()
            {
                ExamSheetId = examSheetId,
                StudentId = student.Id,
                Student = student,
                Mark = 0
            };
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