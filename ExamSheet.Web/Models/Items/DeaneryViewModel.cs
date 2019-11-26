using System.ComponentModel.DataAnnotations;

namespace ExamSheet.Web.Models
{
    public class DeaneryViewModel : IItemViewModel
    {
        [Required]
        [ScaffoldColumn(false)]
        public string Id { get; set; }
        
        [Required(ErrorMessage = "Введіть назву деканату")]
        [Display(Name = "Назва")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Виберіть факультет")]
        [Display(Name = "Факультет")]
        public string FacultyId { get; set; }
    }
}