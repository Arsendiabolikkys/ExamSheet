using ExamSheet.Business.Rating;
using System;
using System.Collections.Generic;

namespace ExamSheet.Business.ExamSheet
{
    public class ExamSheetModel
    {
        public string Id { get; set; }

        public ExamSheetState State { get; set; }

        public DateTime? OpenDate { get; set; }

        public DateTime? CloseDate { get; set; }
        
        public string TeacherId { get; set; }

        public string GroupId { get; set; }

        public string SubjectId { get; set; }

        public string FacultyId { get; set; }

        public short Semester { get; set; }

        public short Year { get; set; }

        public IList<RatingModel> Ratings { get; set; }
    }

    public enum ExamSheetState
    {
        New,
        Open,
        Closed,
        Archived
    }
}