using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSheet.Repository.Subject
{
    public class Subject : IEntity
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }
    }
}
