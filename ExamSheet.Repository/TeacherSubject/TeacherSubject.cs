using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSheet.Repository.TeacherSubject
{
    public class TeacherSubject
    {
        public virtual string SubjectId { get; set; }

        public virtual string TeacherId { get; set; }

        public virtual string GroupId { get; set; }
    }
}
