using NHibernate;

namespace ExamSheet.Repository.Semester
{
    public class SemesterRepository : RepositoryBase<Semester>
    {
        public SemesterRepository(ISessionFactory sessionFactory)
            : base(sessionFactory) { }
    }
}
