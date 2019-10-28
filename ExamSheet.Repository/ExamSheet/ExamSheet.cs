using System;

namespace ExamSheet.Repository.ExamSheet
{
    public class ExamSheet : IEntity
    {
        //TODO: REMOVE ID, MAKE COMPOSITE ID
        public virtual string Id { get; set; }

        public virtual short State { get; set; }

        public virtual DateTime? OpenDate { get; set; }

        public virtual DateTime? CloseDate { get; set; }

        public virtual string TeacherId { get; set; }

        public virtual string GroupId { get; set; }

        public virtual string SubjectId { get; set; }

        public virtual string FacultyId { get; set; }

        public virtual string SemesterId { get; set; }

        //TODO: XML
        public virtual string Ratings { get; set; }
    }
}