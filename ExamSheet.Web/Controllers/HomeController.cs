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
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using ExamSheet.Business.Student;
using ExamSheet.Business.Rating;

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

        protected StudentManager StudentManager { get; set; }

        protected RatingManager RatingManager { get; set; }

        protected int PageSize { get; set; } = 8;

        public HomeController(ExamSheetManager examSheetManager, FacultyManager facultyManager, GroupManager groupManager,
            SubjectManager subjectManager, TeacherManager teacherManager, DeaneryManager deaneryManager, StudentManager studentManager, RatingManager ratingManager)
        {
            this.ExamSheetManager = examSheetManager;
            this.FacultyManager = facultyManager;
            this.GroupManager = groupManager;
            this.SubjectManager = subjectManager;
            this.TeacherManager = teacherManager;
            this.DeaneryManager = deaneryManager;
            this.StudentManager = studentManager;
            this.RatingManager = ratingManager;
        }

        public IActionResult Index(int page = 1, SheetFilterViewModel filter = null)
        {
            if (User.IsInRole(AccountType.Admin))
                return RedirectToAction("Index", "Faculty");
            if (User.IsInRole(AccountType.Teacher))
                return RedirectToAction("Index", "TeacherSheet");

            //GenerateData("28c36d24-580f-409c-92d9-bfdbe4ee4559", 35, 30, 20, 15);
            //GenerateData("93432f51-6cc2-4e02-9523-d8d73daec829", 20, 30, 25, 25);
            
            var model = CreateIndexPageViewModel(page, filter);
            return View(model);
        }

        //TODO: mock exam sheet data
        protected virtual void GenerateData(string subjectId, int notPass, int low, int middle, int high)
        {
            var sheets = ExamSheetManager.FindAll();
            var mmdo = sheets.Where(x => x.SubjectId.Equals(subjectId) && x.State == Business.ExamSheet.ExamSheetState.Open).ToList();
            var rand = new Random(DateTime.Now.Millisecond);
            foreach (var sheet in mmdo)
            {
                var students = StudentManager.FindGroup(sheet.GroupId);
                var ratings = new List<RatingModel>();
                foreach (var st in students)
                {
                    var rating = new RatingModel() { ExamSheetId = sheet.Id, StudentId = st.Id };
                    var prob = rand.Next(0, 100);
                    if (prob <= notPass)
                        rating.Mark = (short)rand.Next(0, 59);
                    else if (prob > notPass && prob <= (notPass + low))
                        rating.Mark = (short)rand.Next(60, 73);
                    else if (prob > (notPass + low) && prob <= (notPass + low + middle))
                        rating.Mark = (short)rand.Next(74, 89);
                    else if (prob > (notPass + low + middle) && prob <= (notPass + low + middle + high))
                        rating.Mark = (short)rand.Next(90, 98);
                    ratings.Add(rating);
                }

                ExamSheetManager.CloseSheet(sheet.Id);
                RatingManager.SaveRatings(ratings);
            }
        }

        protected virtual IndexPageViewModel CreateIndexPageViewModel(int page, SheetFilterViewModel filter = null)
        {
            var model = new IndexPageViewModel();
            var claim = User.Claims.FirstOrDefault(x => x.Type.Equals(Constants.Claims.ReferenceId));
            var deanery = DeaneryManager.GetById(claim.Value);
            var sheetFilter = CreateFilter(filter, deanery.FacultyId);
            var totalCount = ExamSheetManager.GetTotal(sheetFilter);
            model.Page = new PageViewModel(totalCount, page, PageSize);
            model.ExamSheets = ExamSheetManager.FindAll(sheetFilter, page, PageSize).Select(ExamSheetListViewModel).ToList();
            model.Filter = CreateFilterModel(deanery.FacultyId, filter);
            return model;
        }

        protected virtual SheetFilter CreateFilter(SheetFilterViewModel viewFilter, string facultyId)
        {
            return new SheetFilter()
            {
                FacultyId = facultyId,
                GroupId = viewFilter?.GroupId,
                State = viewFilter?.State ?? 0,
                SubjectId = viewFilter?.SubjectId,
                TeacherId = viewFilter?.TeacherId
            };
        }

        protected virtual SheetFilterViewModel CreateFilterModel(string facultyId, SheetFilterViewModel filter = null)
        {
            var model = new SheetFilterViewModel();
            model.StateList = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "Нова", Value = "0" },
                new SelectListItem() { Text = "Відкрита", Value = "1" },
                new SelectListItem() { Text = "Закрита", Value = "2" }
            };
            var subjects = SubjectManager.FindAllForFaculty(facultyId);
            var groups = GroupManager.FindAllForFaculty(facultyId);
            var teachers = TeacherManager.FindAllForFaculty(facultyId);
            model.SubjectList = new List<SelectListItem>() { new SelectListItem() { Text = "Не обрано", Value = string.Empty } };
            model.SubjectList.AddRange(subjects.Select(x => new SelectListItem() { Text = x.Name, Value = x.Id }));
            model.GroupList = new List<SelectListItem>() { new SelectListItem() { Text = "Не обрано", Value = string.Empty } };
            model.GroupList.AddRange(groups.Select(x => new SelectListItem() { Text = x.Name, Value = x.Id }));
            model.TeacherList = new List<SelectListItem>() { new SelectListItem() { Text = "Не обрано", Value = string.Empty } };
            model.TeacherList.AddRange(teachers.Select(x => new SelectListItem() { Text = x.Surname + " " + x.Name, Value = x.Id }));

            if (filter != null)
            {
                model.GroupId = filter.GroupId;
                model.TeacherId = filter.TeacherId;
                model.SubjectId = filter.SubjectId;
                model.State = filter.State;
            }
            return model;
        }

        protected virtual ExamSheetListViewModel ExamSheetListViewModel(ExamSheetModel examSheet)
        {
            var model = new ExamSheetListViewModel();
            model.CloseDate = examSheet.CloseDate;
            model.Faculty = FacultyManager.GetById(examSheet.FacultyId);
            model.Group = GroupManager.GetById(examSheet.GroupId);
            model.Id = examSheet.Id;
            //model.OpenDate = examSheet.OpenDate;
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
