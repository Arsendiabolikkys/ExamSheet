using System;

namespace ExamSheet.Web.Models
{
    public class RatingViewModel
    {
        public string StudentId { get; set; }

        public string SubjectId { get; set; }

        public string SemesterId { get; set; }

        public short Mark { get; set; }
    }
}
