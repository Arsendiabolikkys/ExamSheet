using System;
using System.Collections.Generic;
using System.Linq;
using ExamSheet.Business.Account;
using ExamSheet.Business.Deanery;
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
    public class ExamSheetController : Controller
    {
        protected ExamSheetManager ExamSheetManager { get; set; }

        protected FacultyManager FacultyManager { get; set; }

        protected GroupManager GroupManager { get; set; }

        protected SubjectManager SubjectManager { get; set; }

        protected TeacherManager TeacherManager { get; set; }

        protected AccountManager AccountManager { get; set; }

        protected DeaneryManager DeaneryManager { get; set; }

        protected RatingManager RatingManager { get; set; }

        protected StudentManager StudentManager { get; set; }

        public ExamSheetController(ExamSheetManager examSheetManager, FacultyManager facultyManager, GroupManager groupManager,
            SubjectManager subjectManager, TeacherManager teacherManager, AccountManager accountManager, DeaneryManager deaneryManager,
            RatingManager ratingManager, StudentManager studentManager)
        {
            this.ExamSheetManager = examSheetManager;
            this.FacultyManager = facultyManager;
            this.GroupManager = groupManager;
            this.SubjectManager = subjectManager;
            this.TeacherManager = teacherManager;
            this.AccountManager = accountManager;
            this.DeaneryManager = deaneryManager;
            this.RatingManager = ratingManager;
            this.StudentManager = studentManager;
        }

        protected virtual FacultyModel GetDeaneryFaculty(string email)
        {
            var account = AccountManager.GetByEmail(email);
            var deanery = DeaneryManager.GetById(account.ReferenceId);
            var faculty = FacultyManager.GetById(deanery.FacultyId);
            return faculty;
        }

        [HttpGet]
        [IsInRole(Business.Account.AccountType.Deanery)]
        public IActionResult CreateExamSheet()
        {
            var faculty = GetDeaneryFaculty(User.Identity.Name);
            var model = new ExamSheetViewModel()
            {
                Id = Guid.NewGuid().ToString(),
                //OpenDate = DateTime.Now,
                Faculty = CreateFacultyViewModel(faculty)
            };
            InitSelectItems(model.Faculty.Id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [IsInRole(Business.Account.AccountType.Deanery)]
        public IActionResult CreateExamSheet(ExamSheetViewModel model)
        {
            if (!ModelState.IsValid)
            {
                InitSelectItems(model.Faculty.Id);
                return View(model);
            }
            var examSheet = ExamSheetManager.Get(model.GroupId, model.TeacherId, model.SubjectId, model.Year, model.Semester);
            if (examSheet != null && examSheet.Id != model.Id)
            {
                ModelState.AddModelError(string.Empty, "Така відомість вже існує!");
                InitSelectItems(model.Faculty.Id);
                return View(model);
            }

            SaveOrUpdate(model);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [IsInRole(AccountType.Deanery)]
        public IActionResult ViewSheet(string id)
        {
            var examSheet = ExamSheetManager.GetById(id);
            var model = CreateClosedSheet(examSheet);
            return View(model);
        }

        protected virtual ExamSheetViewModel CreateClosedSheet(ExamSheetModel model)
        {
            return new ExamSheetViewModel()
            {
                Id = model.Id,
                CloseDate = model.CloseDate,
                Group = GroupManager.GetById(model.GroupId),
                Faculty = CreateFacultyViewModel(FacultyManager.GetById(model.FacultyId)),
                Subject = SubjectManager.GetById(model.SubjectId),
                Teacher = TeacherManager.GetById(model.TeacherId),
                Semester = model.Semester,
                State = (Models.ExamSheetState)model.State,
                Year = model.Year,
                Ratings = CreateRatings(model.Id)
            };
        }

        protected virtual List<RatingViewModel> CreateRatings(string examSheetId)
        {
            var ratings = RatingManager.FindAll(examSheetId);
            if (ratings?.Any() ?? false)
                return ratings.Select(CreateRatingViewModel).OrderBy(x => x.Student.Surname).ToList();

            return new List<RatingViewModel>();
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

        [HttpGet]
        [IsInRole(Business.Account.AccountType.Deanery)]
        public IActionResult Edit(string id)
        {
            var examSheet = ExamSheetManager.GetById(id);
            var model = CreateViewModel(examSheet);
            InitSelectItems(model.Faculty.Id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [IsInRole(Business.Account.AccountType.Deanery)]
        public IActionResult Edit(ExamSheetViewModel model)
        {
            if (!ModelState.IsValid)
            {
                InitSelectItems(model.Faculty.Id);
                return View(model);
            }
            var examSheet = ExamSheetManager.Get(model.GroupId, model.TeacherId, model.SubjectId, model.Year, model.Semester);
            if (examSheet != null && examSheet.Id != model.Id)
            {
                ModelState.AddModelError(string.Empty, "Така відомість вже існує!");
                InitSelectItems(model.Faculty.Id);
                return View(model);
            }
            SaveOrUpdate(model);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [IsInRole(Business.Account.AccountType.Deanery)]
        public virtual IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("Index", "Home");

            ExamSheetManager.Remove(id);
            return Json(1);
        }

        public virtual IActionResult Download(string id)
        {
            //TODO: download word
            return RedirectToAction("Index", "Home");
        }

        protected virtual ExamSheetViewModel CreateViewModel(ExamSheetModel model)
        {
            var faculty = FacultyManager.GetById(model.FacultyId);
            return new ExamSheetViewModel()
            {
                Id = model.Id,
                CloseDate = model.CloseDate,
                Faculty = CreateFacultyViewModel(faculty),
                GroupId = model.GroupId,
                //OpenDate = model.OpenDate,
                Semester = model.Semester,
                State = (Models.ExamSheetState)model.State,
                SubjectId = model.SubjectId,
                TeacherId = model.TeacherId,
                Year = model.Year
            };
        }

        protected virtual void SaveOrUpdate(ExamSheetViewModel model)
        {
            var businessModel = CreateModel(model);
            ExamSheetManager.Save(businessModel);
            //TODO: generate word doc and save it
        }

        protected virtual ExamSheetModel CreateModel(ExamSheetViewModel viewModel)
        {
            return new ExamSheetModel()
            {
                Id = viewModel.Id,
                State = (Business.ExamSheet.ExamSheetState)viewModel.State,
                //OpenDate = viewModel.OpenDate,
                TeacherId = viewModel.TeacherId,
                FacultyId = viewModel.Faculty.Id,
                GroupId = viewModel.GroupId,
                SubjectId = viewModel.SubjectId,
                Semester = viewModel.Semester,
                Year = viewModel.Year,
                CloseDate = viewModel.CloseDate
            };
        }

        protected virtual void InitSelectItems(string facultyId)
        {
            InitGroups(facultyId);
            InitSubjects();
            InitTeachers();
        }

        protected virtual void InitTeachers()
        {
            var teachers = TeacherManager.FindAll().Select(CreateTeacherViewModel).ToList();
            ViewData["teachers"] = teachers;
        }

        protected virtual TeacherViewModel CreateTeacherViewModel(TeacherModel teacherModel)
        {
            return new TeacherViewModel()
            {
                Id = teacherModel.Id,
                Name = string.Format("{0} {1}", teacherModel.Surname, teacherModel.Name)
            };
        }

        protected virtual void InitGroups(string facultyId)
        {
            var groups = GroupManager.FindAllForFaculty(facultyId).Select(CreateGroupViewModel).ToList();
            ViewData["groups"] = groups;
        }

        protected virtual GroupViewModel CreateGroupViewModel(GroupModel groupModel)
        {
            return new GroupViewModel()
            {
                Id = groupModel.Id,
                Name = groupModel.Name
            };
        }

        protected virtual void InitSubjects()
        {
            var subjects = SubjectManager.FindAll().Select(CreateSubjectViewModel).ToList();
            ViewData["subjects"] = subjects;
        }

        protected virtual SubjectViewModel CreateSubjectViewModel(SubjectModel subjectModel)
        {
            return new SubjectViewModel()
            {
                Id = subjectModel.Id,
                Name = subjectModel.Name
            };
        }

        protected virtual void InitFaculties()
        {
            var faculties = FacultyManager.FindAll().Select(CreateFacultyViewModel).ToList();
            ViewData["faculties"] = faculties;
        }

        protected virtual FacultyViewModel CreateFacultyViewModel(FacultyModel facultyModel)
        {
            return new FacultyViewModel()
            {
                Id = facultyModel.Id,
                Name = facultyModel.Name
            };
        }
    }
}