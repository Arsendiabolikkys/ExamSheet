using NHibernate;

namespace ExamSheet.Repository.Faculty
{
    public class FacultyRepository : RepositoryBase<Faculty>
    {
        public FacultyRepository(ISessionFactory sessionFactory)
            : base(sessionFactory) { }


    }
}
