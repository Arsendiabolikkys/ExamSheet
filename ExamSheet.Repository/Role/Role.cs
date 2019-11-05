
namespace ExamSheet.Repository.Role
{
    public class Role : IEntity
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }
    }
}
