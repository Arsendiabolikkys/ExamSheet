using System.ComponentModel.DataAnnotations;

namespace ExamSheet.Web.Models
{
    public class TeacherViewModel : IItemViewModel
    {
        [Required]
        [ScaffoldColumn(false)]
        public string Id { get; set; }
        
        [Required]
        [Display(Name = "Ініціали")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Прізвище")]
        public string Surname { get; set; }

        //[Required]
        //[DataType(DataType.EmailAddress)]
        //public string Email { get; set; }
    }
}