using ExamSheet.Business.ExamSheet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamSheet.Web.Models
{
    public class ExamSheetViewModel
    {
        [Required]
        [ScaffoldColumn(false)]
        public string Id { get; set; }

        [Required]
        public ExamSheetState State { get; set; }

        [Display(Name = "Дата відкриття")]
        public DateTime? OpenDate { get; set; }

        [Display(Name = "Дата закриття")]
        public DateTime? CloseDate { get; set; }
        
        public TeacherViewModel Teacher { get; set; }

        public GroupViewModel Group { get; set; }

        public SubjectViewModel Subject { get; set; }
        
        public FacultyViewModel Faculty { get; set; }

        public SemesterViewModel Semester { get; set; }
        
        public IList<RatingViewModel> Ratings { get; set; }
    }

    public enum ExamSheetState
    {
        [Display(Name = "Нова")]
        New,
        [Display(Name = "Відкрита")]
        Open
    }
}
