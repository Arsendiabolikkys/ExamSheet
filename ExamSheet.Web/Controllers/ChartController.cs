using ExamSheet.Business.Account;
using ExamSheet.Business.ExamSheet;
using ExamSheet.Business.Group;
using ExamSheet.Business.Rating;
using ExamSheet.Business.Student;
using ExamSheet.Business.Subject;
using ExamSheet.Web.Attributes;
using ExamSheet.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace ExamSheet.Web.Controllers
{
    [Authorize]
    public class ChartController : Controller
    {
        protected ExamSheetManager ExamSheetManager { get; set; }

        protected RatingManager RatingManager { get; set; }

        protected GroupManager GroupManager { get; set; }

        protected SubjectManager SubjectManager { get; set; }

        protected StudentManager StudentManager { get; set; }

        public ChartController(ExamSheetManager examSheetManager, RatingManager ratingManager, GroupManager groupManager, 
            SubjectManager subjectManager, StudentManager studentManager)
        {
            ExamSheetManager = examSheetManager;
            RatingManager = ratingManager;
            GroupManager = groupManager;
            SubjectManager = subjectManager;
            StudentManager = studentManager;
        }
        
        public IActionResult Index()
        {
            if (User.IsInRole(AccountType.Deanery))
                return RedirectToAction(nameof(DeaneryIndex));
            else if (User.IsInRole(AccountType.Teacher))
                return RedirectToAction(nameof(TeacherIndex));

            return RedirectToAction("Index", "Home");
        }

        [IsInRole(AccountType.Teacher)]
        public IActionResult TeacherIndex()
        {
            var referenceId = User.Claims.FirstOrDefault(x => x.Type.Equals(Constants.Claims.ReferenceId));
            var sheets = ExamSheetManager.FindClosedForTeacher(referenceId.Value);
            if (!sheets?.Any() ?? true)
                return View(new TeacherChartViewModel());

            var groupsIds = sheets.Select(x => x.GroupId).Distinct().ToList();
            var subjectsIds = sheets.Select(x => x.SubjectId).Distinct().ToList();
            var model = new TeacherChartViewModel();
            var groups = GroupManager.GetByIdList(groupsIds);
            var subjects = SubjectManager.GetByIdList(subjectsIds);
            model.GroupList = groups.Select(x => new SelectListItem(x.Name, x.Id)).ToList();
            var subjectList = new List<SelectListItem>();
            var yearsList = new List<SelectListItem>();
            var semesterList = new List<SelectListItem>();

            foreach (var group in groups)
            {
                var groupSheets = sheets.Where(x => x.GroupId == group.Id).ToList();
                if (!groupSheets?.Any() ?? true)
                    continue;

                var listGroup = new SelectListGroup() { Name = group.Name };
                foreach (var sheet in groupSheets)
                {
                    var subject = subjects.FirstOrDefault(x => x.Id == sheet.SubjectId);
                    if (subject == null)
                        continue;

                    semesterList.Add(new SelectListItem() { Text = sheet.Semester.ToString(), Value = sheet.Semester.ToString(), Group = listGroup });
                    yearsList.Add(new SelectListItem() { Text = sheet.Year.ToString(), Value = sheet.Year.ToString(), Group = listGroup });
                    subjectList.Add(new SelectListItem() { Text = subject.Name, Value = subject.Id, Group = listGroup });
                }
            }
            model.YearList = yearsList;
            model.SemesterList = semesterList;
            model.SubjectList = subjectList;
            return View(model);
        }

        [HttpPost]
        public IActionResult GetChartData(string group, string subject, short year, short semester)
        {
            var teacherId = User.Claims.FirstOrDefault(x => x.Type.Equals(Constants.Claims.ReferenceId)).Value;
            var sheet = ExamSheetManager.Get(group, teacherId, subject, year, semester);
            var model = new TeacherChartJsonModel();
            var semesterMarks = new Dictionary<string, short>();
            var rangeMarks = new Dictionary<string, short>();
            var ratings = RatingManager.FindAll(sheet.Id);
            var sum = ratings.Sum(x => x.Mark);
            model.AverageRating = sum / ratings.Count;
            model.StudentsWithLowRating = new List<string>();
            foreach (var rating in ratings)
            {
                var stringRepresentation = RatingMapper.MapRatingToString(rating.Mark);
                var rangeRepresentation = RatingMapper.MapRatingToRange(rating.Mark);
                if (semesterMarks.ContainsKey(stringRepresentation))
                    semesterMarks[stringRepresentation] = (short)(semesterMarks[stringRepresentation] + 1);
                else
                    semesterMarks[stringRepresentation] = 1;
                if (rangeMarks.ContainsKey(rangeRepresentation))
                    rangeMarks[rangeRepresentation] = (short)(rangeMarks[rangeRepresentation] + 1);
                else
                    rangeMarks[rangeRepresentation] = 1;

                if (rating.Mark < model.AverageRating)
                {
                    var student = StudentManager.GetById(rating.StudentId);
                    model.StudentsWithLowRating.Add(string.Format("{0} {1}", student.Surname, student.Name));
                }
            }
            var sorted = semesterMarks.OrderBy(key => key.Key);
            model.SemesterMarks = sorted.ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);
            sorted = rangeMarks.OrderBy(key => key.Key);
            model.RangeMarks = sorted.ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);
            model.AverageRating = sum / ratings.Count;
            return Json(model);
        }

        [IsInRole(AccountType.Deanery)]
        public IActionResult DeaneryIndex()
        {
            var referenceId = User.Claims.FirstOrDefault(x => x.Type.Equals(Constants.Claims.ReferenceId));
            return View();
        }
    }
}