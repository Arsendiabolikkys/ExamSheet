using NHibernate;

namespace ExamSheet.Repository.Group
{
    public class GroupRepository : RepositoryBase<Group>
    {
        public GroupRepository(ISessionFactory sessionFactory)
            : base(sessionFactory) { }
    }
}
