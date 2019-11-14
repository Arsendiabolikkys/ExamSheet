using ExamSheet.Business.Faculty;
using ExamSheet.Business.Group;
using ExamSheet.Business.Rating;
using ExamSheet.Business.Semester;
using ExamSheet.Business.Subject;
using ExamSheet.Business.Teacher;
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

        //public TeacherModel Teacher { get; set; }

        //public GroupModel Group { get; set; }

        //public SubjectModel Subject { get; set; }

        //public FacultyModel Faculty { get; set; }

        //public SemesterModel Semester { get; set; }

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