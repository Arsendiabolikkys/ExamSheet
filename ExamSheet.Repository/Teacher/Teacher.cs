using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSheet.Repository.Teacher
{
    public class Teacher : IEntity
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Surname { get; set; }

        //public virtual string SubjectId { get; set; }
    }
}
