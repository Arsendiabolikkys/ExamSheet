using ExamSheet.Business.ExamSheet;
using ExamSheet.Business.Faculty;
using ExamSheet.Business.Group;
using ExamSheet.Business.Subject;
using ExamSheet.Business.Teacher;
using Microsoft.AspNetCore.Mvc;
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

        //TODO: 
        //[Required]
        //[Display(Name = "Дата відкриття")]
        //[UIHint("DateTime")]
        //public DateTime? OpenDate { get; set; }
        
        [Required(ErrorMessage = "Виберіть групу")]
        [Display(Name = "Група")]
        public string GroupId { get; set; }

        [Required(ErrorMessage = "Виберіть предмет")]
        [Display(Name = "Предмет")]
        public string SubjectId { get; set; }

        [Required(ErrorMessage = "Виберіть викладача")]
        [Display(Name = "Викладач")]
        public string TeacherId { get; set; }

        [Display(Name = "Дата закриття")]
        public DateTime? CloseDate { get; set; }

        [Required(ErrorMessage = "Введіть семестр відомості")]
        [Display(Name = "Семестр")]
        [UIHint("SemesterNumber")]
        public short Semester { get; set; }

        [Required(ErrorMessage = "Введіть рік відомості")]
        [Display(Name = "Рік")]
        [UIHint("Year")]
        public short Year { get; set; }
        
        public FacultyViewModel Faculty { get; set; }
        
        [Display(Name = "Викладач")]
        public TeacherModel Teacher { get; set; }

        public SubjectModel Subject { get; set; }

        public GroupModel Group { get; set; }

        [Display(Name = "Оцінки")]
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
