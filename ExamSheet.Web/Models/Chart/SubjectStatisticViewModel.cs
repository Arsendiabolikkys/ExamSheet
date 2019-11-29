using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSheet.Web.Models
{
    public class SubjectStatisticViewModel
    {
        public List<SelectListItem> SubjectList { get; set; }

        public string FacultyId { get; set; }
    }

    public class SubjectChartJsonModel
    {
        public Dictionary<string, float> AverageRatings { get; set; }

        public Dictionary<short, short> RatingFrequency { get; set; }

        public Dictionary<short, double>[] NormalDistributions { get; set; }

        public List<string> NormalDistributionLabels { get; set; }
    }
}
