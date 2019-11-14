using ExamSheet.Business.Faculty;
using ExamSheet.Business.Group;
using ExamSheet.Business.Subject;
using ExamSheet.Business.Teacher;
using System;

namespace ExamSheet.Web.Models
{
    public class ExamSheetListViewModel
    {
        public string Id { get; set; }
        
        public ExamSheetState State { get; set; }
        
        public DateTime? OpenDate { get; set; }
        
        public FacultyModel Faculty { get; set; }
        
        public GroupModel Group { get; set; }
        
        public SubjectModel Subject { get; set; }
        
        public TeacherModel Teacher { get; set; }

        public DateTime? CloseDate { get; set; }
        
        public short Semester { get; set; }
        
        public short Year { get; set; }
    }
}
