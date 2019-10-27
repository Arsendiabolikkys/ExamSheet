using ExamSheet.Business.Subject;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSheet.Business.Teacher
{
    public class TeacherModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public IEnumerable<SubjectModel> Subjects { get; set; }
    }
}
