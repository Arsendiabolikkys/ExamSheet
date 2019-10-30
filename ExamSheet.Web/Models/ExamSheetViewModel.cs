using ExamSheet.Business.ExamSheet;
using System;
using System.ComponentModel.DataAnnotations;

namespace ExamSheet.Web.Models
{
    public class ExamSheetViewModel
    {
        [Required]
        //[UIHint("ExamSheetState")]
        public ExamSheetState State { get; set; }

        public DateTime? OpenDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        public TeacherViewModel Teacher { get; set; }

        public GroupViewModel Group { get; set; }

        public SubjectViewModel Subject { get; set; }

        public FacultyViewModel Faculty { get; set; }

        public SemesterViewModel Semester { get; set; }

        //TODO: XML FIELD / strongly typed class
        public string Ratings { get; set; }
    }
}
