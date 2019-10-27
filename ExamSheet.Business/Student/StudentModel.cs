using ExamSheet.Business.Group;

namespace ExamSheet.Business.Student
{
    public class StudentModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public GroupModel Group { get; set; }
    }
}
