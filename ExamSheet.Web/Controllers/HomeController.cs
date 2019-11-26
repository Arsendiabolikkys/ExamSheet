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

        protected int PageSize { get; set; } = 1;

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

        public IActionResult Index(int page = 1, SheetFilterViewModel filter = null)
        {
            if (User.IsInRole(AccountType.Admin))
                return RedirectToAction("Index", "Faculty");
            if (User.IsInRole(AccountType.Teacher))
                return RedirectToAction("Index", "TeacherSheet");

            var model = CreateIndexPageViewModel(page, filter);
            return View(model);
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
