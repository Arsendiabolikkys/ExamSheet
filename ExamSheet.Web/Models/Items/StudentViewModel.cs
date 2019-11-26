using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamSheet.Web.Models
{
    public class StudentViewModel : IItemViewModel
    {
        [Required]
        [ScaffoldColumn(false)]
        public string Id { get; set; }

        [Required(ErrorMessage = "Введіть ініціали")]
        [Display(Name = "Ініціали")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введіть прізвище")]
        [Display(Name = "Прізвище")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Виберіть групу")]
        [Display(Name = "Група")]
        public string GroupId { get; set; }
    }
}