using System;

namespace ExamSheet.Repository.ExamSheet
{
    public class ExamSheet : IEntity
    {
        //TODO: REMOVE ID, MAKE COMPOSITE ID
        //public virtual string Id { get; set; }

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

        public override bool Equals(object obj)
        {
            var other = obj as ExamSheet;
            if (other == null)
                return false;

            return other.TeacherId.Equals(TeacherId) && other.GroupId.Equals(GroupId) && other.SubjectId.Equals(SubjectId);
        }

        public override int GetHashCode()
        {
            int hash = GetType().GetHashCode();
            hash = (hash * 13) ^ TeacherId.GetHashCode();
            hash = (hash * 13) ^ SubjectId.GetHashCode();
            hash = (hash * 13) ^ GroupId.GetHashCode();
            return hash; 
        }
    }
}