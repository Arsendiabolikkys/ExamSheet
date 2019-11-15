using ExamSheet.Business.ExamSheet;
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

        [Required]
        [Display(Name = "Дата відкриття")]
        [UIHint("DateTime")]
        public DateTime? OpenDate { get; set; }
        
        [Required]
        [Display(Name = "Група")]
        public string GroupId { get; set; }

        [Required]
        [Display(Name = "Предмет")]
        public string SubjectId { get; set; }

        [Required]
        [Display(Name = "Викладач")]
        public string TeacherId { get; set; }

        [Display(Name = "Дата закриття")]
        public DateTime? CloseDate { get; set; }

        [Required]
        [Display(Name = "Семестр")]
        [UIHint("SemesterNumber")]
        public short Semester { get; set; }

        [Required]
        [Display(Name = "Рік")]
        [UIHint("Year")]
        public short Year { get; set; }
        
        public FacultyViewModel Faculty { get; set; }

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
