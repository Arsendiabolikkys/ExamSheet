using System;

namespace ExamSheet.Repository.Rating
{
    public class Rating : IEntity
    {
        public virtual string StudentId { get; set; }
        
        public virtual string ExamSheetId { get; set; }

        public virtual short Mark { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Rating;
            if (other == null)
                return false;

            return other.StudentId.Equals(StudentId) && other.ExamSheetId.Equals(ExamSheetId);
        }

        public override int GetHashCode()
        {
            int hash = GetType().GetHashCode();
            hash = (hash * 13) ^ StudentId.GetHashCode();
            hash = (hash * 13) ^ ExamSheetId.GetHashCode();
            return hash;
        }
    }
}