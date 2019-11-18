using ExamSheet.Business.Group;
using ExamSheet.Business.Subject;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSheet.Web.Models
{
    public class GroupStatisticViewModel
    {
        public List<SelectListItem> GroupList { get; set; }

        public List<SelectListItem> SubjectList { get; set; }

        public List<SelectListItem> YearList { get; set; }

        public List<SelectListItem> SemesterList { get; set; }

        public List<SelectListItem> TeacherList { get; set; }

        public string TeacherId { get; set; }
    }

    public class GroupChartJsonModel
    {
        public Dictionary<string, short> SemesterMarks { get; set; }

        public Dictionary<string, short> RangeMarks { get; set; }

        public List<StudentRating> StudentsRating { get; set; }
    }

    public class StudentRating
    {
        public string Surname { get; set; }

        public string Name { get; set; }

        public string StringRepresentation { get; set; }

        public short Rating { get; set; }
    }
}
