using System;

namespace ExamSheet.Repository.Deanery
{
    public class Deanery : IEntity
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string FacultyId { get; set; }
    }
}
