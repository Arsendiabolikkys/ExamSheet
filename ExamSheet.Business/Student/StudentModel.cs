using ExamSheet.Business.Group;

namespace ExamSheet.Business.Student
{
    public class StudentModel : IItem
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string GroupId { get; set; }
    }
}
