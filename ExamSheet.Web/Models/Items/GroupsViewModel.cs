using ExamSheet.Business.Faculty;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSheet.Web.Models
{
    public class GroupsViewModel : ItemsViewModel
    {
        public List<SelectListItem> Faculties { get; set; }

        [Display(Name = "Факультет")]
        public string FacultyId { get; set; }
    }
}
