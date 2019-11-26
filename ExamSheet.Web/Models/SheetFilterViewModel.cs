using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSheet.Web.Models
{
    public class SheetFilterViewModel
    {
        [Display(Name = "Статус")]
        public short State { get; set; }

        public List<SelectListItem> StateList { get; set; }

        [Display(Name = "Предмет")]
        public string SubjectId { get; set; }

        public List<SelectListItem> SubjectList { get; set; }

        [Display(Name = "Викладач")]
        public string TeacherId { get; set; }

        public List<SelectListItem> TeacherList { get; set; }

        [Display(Name = "Група")]
        public string GroupId { get; set; }

        public List<SelectListItem> GroupList { get; set; }
        
        public List<SelectListItem> FacultyList { get; set; }

        [Display(Name = "Факультет")]
        public string FacultyId { get; set; }
    }
}
