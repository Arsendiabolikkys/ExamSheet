using NHibernate;

namespace ExamSheet.Repository.Role
{
    public class RoleRepository : RepositoryBase<Role>
    {
        public RoleRepository(ISessionFactory sessionFactory)
            : base(sessionFactory) { }
    }
}
