using ExamSheet.Business.Student;
using System;
using System.ComponentModel.DataAnnotations;

namespace ExamSheet.Web.Models
{
    public class RatingViewModel
    {
        public StudentModel Student { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public string StudentId { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public string ExamSheetId { get; set; }

        [Required(ErrorMessage = "Введіть оцінку")]
        [Range(0, 100, ErrorMessage = "Оцінка має бути в межах від 0 до 100")]
        public short Mark { get; set; }
    }
}
