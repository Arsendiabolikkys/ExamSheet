using System;
using System.ComponentModel.DataAnnotations;

namespace ExamSheet.Web.Models
{
    public class SubjectViewModel : IItemViewModel
    {
        [Required]
        [ScaffoldColumn(false)]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Предмет")]
        public string Name { get; set; }
    }
}
