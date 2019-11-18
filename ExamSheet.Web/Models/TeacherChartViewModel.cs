using ExamSheet.Business.Group;
using ExamSheet.Business.Subject;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSheet.Web.Models
{
    public class TeacherChartViewModel
    {
        public List<SelectListItem> GroupList { get; set; }

        public List<SelectListItem> SubjectList { get; set; }

        public List<SelectListItem> YearList { get; set; }

        public List<SelectListItem> SemesterList { get; set; }
    }

    public class TeacherChartJsonModel
    {
        public Dictionary<string, short> SemesterMarks { get; set; }

        public Dictionary<string, short> RangeMarks { get; set; }

        public float AverageRating { get; set; }

        public List<string> StudentsWithLowRating { get; set; }
    }

    public class SemesterMarksJsonModel
    {
        //TODO: add bar with rating 0-10, 10-20 .... 90-100
        //TODO: add median rating, show students for which rating is lower than median (add description)

        public Dictionary<string, short> Marks { get; set; }
    }
}
