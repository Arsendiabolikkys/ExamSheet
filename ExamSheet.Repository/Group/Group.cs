using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSheet.Repository.Group
{
    public class Group
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string SubjectId { get; set; }
    }
}
