using ExamSheet.Business.Account;
using ExamSheet.Business.Deanery;
using ExamSheet.Business.ExamSheet;
using ExamSheet.Business.Group;
using ExamSheet.Business.Rating;
using ExamSheet.Business.Student;
using ExamSheet.Business.Subject;
using ExamSheet.Business.Teacher;
using ExamSheet.Web.Attributes;
using ExamSheet.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
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

        protected DeaneryManager DeaneryManager { get; set; }

        protected TeacherManager TeacherManager { get; set; }

        public ChartController(ExamSheetManager examSheetManager, RatingManager ratingManager, GroupManager groupManager, 
            SubjectManager subjectManager, StudentManager studentManager, DeaneryManager deaneryManager, TeacherManager teacherManager)
        {
            ExamSheetManager = examSheetManager;
            RatingManager = ratingManager;
            GroupManager = groupManager;
            SubjectManager = subjectManager;
            StudentManager = studentManager;
            DeaneryManager = deaneryManager;
            TeacherManager = teacherManager;
        }
        
        public IActionResult Index()
        {
            var referenceId = User.Claims.FirstOrDefault(x => x.Type.Equals(Constants.Claims.ReferenceId));
            var model = new GroupStatisticViewModel();
            if (User.IsInRole(AccountType.Deanery))
            {
                var deanery = DeaneryManager.GetById(referenceId.Value);
                var sheets = ExamSheetManager.FindClosedForFaculty(deanery.FacultyId);
                model = CreateGroupStatisticViewModel(sheets);
            }
            else if (User.IsInRole(AccountType.Teacher))
            {
                var sheets = ExamSheetManager.FindClosedForTeacher(referenceId.Value);
                model = CreateGroupStatisticViewModel(sheets);
                model.TeacherId = referenceId.Value;
            }

            return View(model);
        }

        [IsInRole(AccountType.Deanery)]
        public IActionResult Subjects()
        {
            var referenceId = User.Claims.FirstOrDefault(x => x.Type.Equals(Constants.Claims.ReferenceId));
            var deanery = DeaneryManager.GetById(referenceId.Value);
            var sheets = ExamSheetManager.FindClosedForFaculty(deanery.FacultyId);
            var subjectsIds = sheets.Select(x => x.SubjectId).Distinct().ToList();
            var subjects = SubjectManager.GetByIdList(subjectsIds);
            var model = new SubjectStatisticViewModel();
            model.SubjectList = new List<SelectListItem>();
            foreach (var subject in subjects)
            {
                model.SubjectList.Add(new SelectListItem() { Text = subject.Name, Value = subject.Id });
            }
            model.FacultyId = deanery.FacultyId;
            return View(model);
        }

        [HttpPost]
        [IsInRole(AccountType.Deanery)]
        public IActionResult GetSubjectData(string subject, string faculty)
        {
            var sheets = ExamSheetManager.Get(faculty, subject);
            var ids = sheets.Select(x => x.Id);
            var groupedByYears = sheets.GroupBy(x => x.Year);
            var model = new SubjectChartJsonModel();
            model.AverageRatings = new Dictionary<string, float>();
            model.RatingFrequency = new Dictionary<short, short>();
            var ratings = new List<RatingModel>();
            foreach (var year in groupedByYears)
            {
                var groupedBySemesters = year.GroupBy(x => x.Semester);
                foreach (var semester in groupedBySemesters)
                {
                    var sum = 0;
                    var count = 0;
                    foreach (var item in semester)
                    {
                        ratings.AddRange(item.Ratings);
                        sum += item.Ratings.Sum(x => x.Mark);
                        count += item.Ratings.Count;
                    }
                    var average = sum / (float)count;
                    var title = string.Format("{0} {1}", year.Key, semester.Key);
                    model.AverageRatings.Add(title, average);
                }
            }
            foreach (var rating in ratings)
            {
                if (model.RatingFrequency.ContainsKey(rating.Mark))
                {
                    model.RatingFrequency[rating.Mark]++;
                }
                else
                {
                    model.RatingFrequency.Add(rating.Mark, 1);
                }

            }
            var M = ratings.Sum(x => x.Mark) / ratings.Count;
            var D = ratings.Sum(x => Math.Pow(x.Mark - M, 2)) / (ratings.Count - 1);
            var sigma = 2;//Math.Sqrt(D);
            model.NormalDistribution = new Dictionary<short, double>();
            for (short i = 0; i < 100; ++i)
            {
                double func = (1 / (sigma * Math.Sqrt(2 * Math.PI))) * Math.Pow(Math.E, -(Math.Pow((i - M), 2)) / (2 * sigma * sigma));
                model.NormalDistribution.Add(i, func);
            }
            //TODO: normal
            //Мат очікування - середнє арифм
            //Дисперсія - https://ru.wikihow.com/%D0%BF%D0%BE%D1%81%D1%87%D0%B8%D1%82%D0%B0%D1%82%D1%8C-%D0%B4%D0%B8%D1%81%D0%BF%D0%B5%D1%80%D1%81%D0%B8%D1%8E-%D1%81%D0%BB%D1%83%D1%87%D0%B0%D0%B9%D0%BD%D0%BE%D0%B9-%D0%B2%D0%B5%D0%BB%D0%B8%D1%87%D0%B8%D0%BD%D1%8B
            // Среднеквадратическое отклонение - корень з дисперсії
            // Y = { 1/[ σ * sqrt(2π) ] } * e-(x - μ)2/2σ2  (https://stattrek.com/probability-distributions/normal.aspx)

            // X - 60 балов, 74, 90

            return Json(model);
        }

        protected virtual GroupStatisticViewModel CreateGroupStatisticViewModel(IEnumerable<ExamSheetModel> sheets)
        {
            var model = new GroupStatisticViewModel();
            var groupsIds = sheets.Select(x => x.GroupId).Distinct().ToList();
            var subjectsIds = sheets.Select(x => x.SubjectId).Distinct().ToList();
            var teachersIds = sheets.Select(x => x.TeacherId).Distinct().ToList();
            var teachers = TeacherManager.GetByIdList(teachersIds);
            var groups = GroupManager.GetByIdList(groupsIds);
            var subjects = SubjectManager.GetByIdList(subjectsIds);
            model.GroupList = groups.Select(x => new SelectListItem(x.Name, x.Id)).ToList();
            model.SemesterList = new List<SelectListItem>();
            model.SubjectList = new List<SelectListItem>();
            model.TeacherList = new List<SelectListItem>();
            model.YearList = new List<SelectListItem>();

            foreach (var group in groups)
            {
                var groupSheets = sheets.Where(x => x.GroupId == group.Id).ToList();
                if (!groupSheets?.Any() ?? true)
                    continue;

                var listGroup = new SelectListGroup() { Name = group.Name };
                foreach (var sheet in groupSheets)
                {
                    model.SemesterList.Add(new SelectListItem() { Text = sheet.Semester.ToString(), Value = sheet.Semester.ToString(), Group = listGroup });
                    model.YearList.Add(new SelectListItem() { Text = sheet.Year.ToString(), Value = sheet.Year.ToString(), Group = listGroup });
                    var subject = subjects.FirstOrDefault(x => x.Id == sheet.SubjectId);
                    if (subject != null)
                    {
                        model.SubjectList.Add(new SelectListItem() { Text = subject.Name, Value = subject.Id, Group = listGroup });
                    }
                    var teacher = teachers.FirstOrDefault(x => x.Id == sheet.TeacherId);
                    if (teacher != null)
                    {
                        model.TeacherList.Add(new SelectListItem() { Text = string.Format("{0} {1}", teacher.Surname, teacher.Name), Value = teacher.Id, Group = listGroup });
                    }
                }
            }
            return model;
        }

        [HttpPost]
        public IActionResult GetChartData(string group, string subject, short year, short semester, string teacher)
        {
            var sheet = ExamSheetManager.Get(group, teacher, subject, year, semester);
            var model = new GroupChartJsonModel();
            var semesterMarks = new Dictionary<string, short>();
            var rangeMarks = new Dictionary<string, short>();
            var ratings = RatingManager.FindAll(sheet.Id);
            model.StudentsRating = new List<StudentRating>();
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

                var student = StudentManager.GetById(rating.StudentId);
                model.StudentsRating.Add(new StudentRating() { Surname = student.Surname, Name = student.Name, Rating = rating.Mark, StringRepresentation = stringRepresentation });
            }
            var sorted = semesterMarks.OrderBy(key => key.Key);
            model.SemesterMarks = sorted.ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);
            sorted = rangeMarks.OrderBy(key => key.Key);
            model.RangeMarks = sorted.ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);
            model.StudentsRating = model.StudentsRating.OrderByDescending(x => x.Rating).ToList();
            return Json(model);
        }
    }
}