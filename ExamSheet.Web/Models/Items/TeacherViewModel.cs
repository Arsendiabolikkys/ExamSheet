using System.ComponentModel.DataAnnotations;

namespace ExamSheet.Web.Models
{
    public class TeacherViewModel : IItemViewModel
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
    }
}