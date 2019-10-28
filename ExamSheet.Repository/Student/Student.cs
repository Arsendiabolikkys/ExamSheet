
namespace ExamSheet.Repository.Student
{
    public class Student : IEntity
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Surname { get; set; }

        public virtual string GroupId { get; set; }
    }
}
