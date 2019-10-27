using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSheet.Repository.Semester
{
    public class Semester
    {
        public virtual string Id { get; set; }

        public virtual short Year { get; set; }

        public virtual short Number { get; set; }
    }
}
