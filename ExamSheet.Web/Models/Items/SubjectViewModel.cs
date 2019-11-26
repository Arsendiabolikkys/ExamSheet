using System;
using System.ComponentModel.DataAnnotations;

namespace ExamSheet.Web.Models
{
    public class SubjectViewModel : IItemViewModel
    {
        [Required]
        [ScaffoldColumn(false)]
        public string Id { get; set; }

        [Required(ErrorMessage = "Введіть назву предмету")]
        [Display(Name = "Предмет")]
        public string Name { get; set; }
    }
}
