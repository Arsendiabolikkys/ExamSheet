using System.ComponentModel.DataAnnotations;

namespace ExamSheet.Web.Models
{
    public class FacultyViewModel
    {
        [Required]
        [ScaffoldColumn(false)]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Факультет")]
        public string Name { get; set; }
    }
}
