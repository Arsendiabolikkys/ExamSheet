using ExamSheet.Business.Faculty;
using ExamSheet.Business.Group;
using ExamSheet.Business.Semester;
using ExamSheet.Business.Subject;
using ExamSheet.Business.Teacher;
using System;

namespace ExamSheet.Business.ExamSheet
{
    public class ExamSheetModel
    {
        public string Id { get; set; }

        public ExamSheetState State { get; set; }

        public DateTime? OpenDate { get; set; }

        public DateTime? CloseDate { get; set; }

        public TeacherModel Teacher { get; set; }

        public GroupModel Group { get; set; }

        public SubjectModel Subject { get; set; }

        public FacultyModel Faculty { get; set; }

        public SemesterModel Semester { get; set; }

        //TODO: XML FIELD
        public string Ratings { get; set; }
    }

    public enum ExamSheetState
    {
        New,
        Open,
        Closed,
        Archived
    }
}