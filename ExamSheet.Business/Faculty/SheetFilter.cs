using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSheet.Business.Faculty
{
    public class SheetFilter
    {
        public string FacultyId { get; set; }

        public string TeacherId { get; set; }

        public string SubjectId { get; set; }

        public string GroupId { get; set; }

        public short State { get; set; }
    }
}
