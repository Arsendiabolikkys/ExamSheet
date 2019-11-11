using System.ComponentModel.DataAnnotations;

namespace ExamSheet.Web.Models
{
    //TODO: create teachers with admin role ?
    //TODO: login first time - create password???
    //TODO: add new table - accounts - if create teacher - role teacher

    public class TeacherViewModel
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