﻿using ExamSheet.Business.Faculty;

namespace ExamSheet.Business.Deanery
{
    public class DeaneryModel : IItem
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string FacultyId { get; set; }
        //public virtual FacultyModel Faculty { get; set; }
    }
}
