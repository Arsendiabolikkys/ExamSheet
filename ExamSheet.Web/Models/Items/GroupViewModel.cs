using System;
using System.ComponentModel.DataAnnotations;

namespace ExamSheet.Web.Models
{
    public class GroupViewModel : IItemViewModel
    {
        [Required]
        [ScaffoldColumn(false)]
        public string Id { get; set; }

        [Required(ErrorMessage = "Введіть назву групи")]
        [Display(Name = "Група")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Виберіть факультет")]
        [Display(Name = "Факультет")]
        public string FacultyId { get; set; }
    }
}
