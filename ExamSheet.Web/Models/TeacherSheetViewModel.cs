using ExamSheet.Business.ExamSheet;
using ExamSheet.Business.Faculty;
using ExamSheet.Business.Group;
using ExamSheet.Business.Subject;
using ExamSheet.Business.Teacher;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamSheet.Web.Models
{
    public class TeacherSheetViewModel
    {
        public string Id { get; set; }
        
        public ExamSheetState State { get; set; }
        
        [Display(Name = "Дата відкриття")]
        [UIHint("DateTime")]
        public DateTime? OpenDate { get; set; }
        
        [Display(Name = "Група")]
        public GroupModel Group { get; set; }
        
        [Display(Name = "Предмет")]
        public SubjectModel Subject { get; set; }
        
        [Display(Name = "Викладач")]
        public TeacherModel Teacher { get; set; }

        [Display(Name = "Дата закриття")]
        public DateTime? CloseDate { get; set; }
        
        [Display(Name = "Семестр")]
        [UIHint("SemesterNumber")]
        public short Semester { get; set; }
        
        [Display(Name = "Рік")]
        [UIHint("Year")]
        public short Year { get; set; }
        
        [Display(Name = "Факультет")]
        public FacultyModel Faculty { get; set; }
        
        [Display(Name = "Оцінки")]
        public IList<RatingViewModel> Ratings { get; set; }
    }
}
