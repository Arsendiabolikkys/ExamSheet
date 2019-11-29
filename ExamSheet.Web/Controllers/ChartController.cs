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
using ExamSheet.Web.Models.Chart;
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
            var sheets = ExamSheetManager.Get(faculty, subject, true);
            var model = new SubjectChartJsonModel();
            if (!sheets?.Any() ?? true)
                return Json(model);
            var ids = sheets.Select(x => x.Id);
            var groupedByYears = sheets.GroupBy(x => x.Year).OrderBy(x => x.Key);
            model.AverageRatings = new Dictionary<string, float>();
            model.RatingFrequency = new Dictionary<short, short>();
            var ratings = new List<RatingModel>();
            foreach (var year in groupedByYears)
            {
                var groupedBySemesters = year.GroupBy(x => x.Semester).OrderByDescending(x => x.Key);
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
            var sigma = Math.Sqrt(D);
            var size = 4;
            model.NormalDistributions = new Dictionary<short, double>[size];
            for (int i = 0; i < size; ++i)
            {
                model.NormalDistributions[i] = new Dictionary<short, double>();
            }
            var eScore = calcZScore(60, M, sigma);
            var cScore = calcZScore(74, M, sigma);
            var aScore = calcZScore(90, M, sigma);
            var eProbability = ZTable.GetProbability(eScore);
            var cProbability = ZTable.GetProbability(cScore);
            var aProbability = ZTable.GetProbability(aScore);

            var probFail = eProbability * 100;
            var probSatisfactorily = (cProbability - eProbability) * 100;
            var probGood = (aProbability - cProbability) * 100;
            var probExcellent = (1 - aProbability) * 100;
            model.NormalDistributionLabels = new List<string>()
            {
                string.Format("Ймовірність не здати предмет: {0}%", probFail.ToString("0.0")),
                string.Format("Ймовірність здати на 60-74: {0}%", probSatisfactorily.ToString("0.0")),
                string.Format("Ймовірність здати на 74-90: {0}%", probGood.ToString("0.0")),
                string.Format("Ймовірність здати на 90+: {0}%", probExcellent.ToString("0.0"))
            };
            for (short i = 0; i < 100; ++i)
            {
                double func = (1 / (sigma * Math.Sqrt(2 * Math.PI))) * Math.Exp(-(Math.Pow((i - M), 2)) / (2 * sigma * sigma));
                if (i < 60)
                {
                    model.NormalDistributions[0].Add(i, func * 1000);
                }
                else if (i < 74)
                {
                    model.NormalDistributions[1].Add(i, func * 1000);
                }
                else if (i < 90)
                {
                    model.NormalDistributions[2].Add(i, func * 1000);
                }
                else
                {
                    model.NormalDistributions[3].Add(i, func * 1000);
                }
            }
            return Json(model);
        }

        protected double calcZScore(double x, double M, double sigma)
        {
            return Math.Round(((x - M) / sigma), 1);
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
            model.GroupList = groups.OrderBy(x => x.Name).Select(x => new SelectListItem(x.Name, x.Id)).ToList();
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
                    var subject = subjects.FirstOrDefault(x => x.Id == sheet.SubjectId);
                    var subjectValidName = subject.Name.Replace('\'', ' ').Replace('-', ' ').Replace(':', ' ').Replace('.', ' ').Replace('(', ' ').Replace(')', ' ');
                    if (subject != null && !model.SubjectList.Any(x => x.Value == subject.Id && x.Group.Name == group.Name))
                    {
                        model.SubjectList.Add(new SelectListItem() { Text = subjectValidName, Value = subject.Id, Group = listGroup });
                    }
                    
                    var subjectGroupName = string.Format("{0} {1}", group.Name, subjectValidName);
                    var listGroupSubject = new SelectListGroup() { Name = subjectGroupName };
                    if (!model.SemesterList.Any(x => x.Value == sheet.Semester.ToString() && x.Group.Name == subjectGroupName))
                    {
                        model.SemesterList.Add(new SelectListItem() { Text = sheet.Semester.ToString(), Value = sheet.Semester.ToString(), Group = listGroupSubject });
                    }
                    if (!model.YearList.Any(x => x.Value == sheet.Year.ToString() && x.Group.Name == subjectGroupName))
                    {
                        model.YearList.Add(new SelectListItem() { Text = sheet.Year.ToString(), Value = sheet.Year.ToString(), Group = listGroupSubject });
                    }
                    var teacher = teachers.FirstOrDefault(x => x.Id == sheet.TeacherId);
                    if (teacher != null && !model.TeacherList.Any(x => x.Value == teacher.Id && x.Group.Name == subjectGroupName))
                    {
                        model.TeacherList.Add(new SelectListItem() { Text = string.Format("{0} {1}", teacher.Surname, teacher.Name), Value = teacher.Id, Group = listGroupSubject });
                    }
                }
            }
            return model;
        }

        [HttpPost]
        public IActionResult GetChartData(string group, string subject, short year, short semester, string teacher)
        {
            var sheet = ExamSheetManager.Get(group, teacher, subject, year, semester, true);
            var model = new GroupChartJsonModel();
            if (sheet == null)
                return Json(model);
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