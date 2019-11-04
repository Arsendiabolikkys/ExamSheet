using System;
using System.ComponentModel.DataAnnotations;

namespace ExamSheet.Web.Models
{
    //TODO: create subjects page for admin

    public class SubjectViewModel
    {
        [Required]
        [ScaffoldColumn(false)]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Предмет")]
        public string Name { get; set; }
    }
}
