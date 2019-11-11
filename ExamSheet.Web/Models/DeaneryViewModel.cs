using System.ComponentModel.DataAnnotations;

namespace ExamSheet.Web.Models
{
    //TODO: add Dekan Surname -> Initials
    public class DeaneryViewModel : IItemViewModel
    {
        [Required]
        [ScaffoldColumn(false)]
        public string Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string FacultyId { get; set; }
    }
}