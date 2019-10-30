using System;
using System.ComponentModel.DataAnnotations;

namespace ExamSheet.Web.Models
{
    public class SemesterViewModel
    {
        [Required]
        [ScaffoldColumn(false)]
        public string Id { get; set; }

        [Required]
        //TODO: https://stackoverflow.com/questions/13128701/how-display-only-years-in-input-bootstrap-datepicker
        public short Year { get; set; }

        [Required]
        //TODO: first - second semester
        public short Number { get; set; }
    }
}
