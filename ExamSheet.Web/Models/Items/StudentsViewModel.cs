using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSheet.Web.Models
{
    public class StudentsViewModel : ItemsViewModel
    {
        public List<SelectListItem> Faculties { get; set; }

        [Display(Name = "Факультет")]
        public string FacultyId { get; set; }

        public List<SelectListItem> Groups { get; set; }

        [Display(Name = "Група")]
        public string GroupId { get; set; }
    }
}
