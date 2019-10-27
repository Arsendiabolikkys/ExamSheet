using NHibernate;

namespace ExamSheet.Repository.Subject
{
    public class SubjectRepository : RepositoryBase<Subject>
    {
        public SubjectRepository(ISessionFactory sessionFactory)
            : base(sessionFactory) { }
    }
}
