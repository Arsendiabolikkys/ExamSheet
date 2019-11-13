using System;

namespace ExamSheet.Repository.Group
{
    public class Group : IEntity
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string FacultyId { get; set; }
    }
}
