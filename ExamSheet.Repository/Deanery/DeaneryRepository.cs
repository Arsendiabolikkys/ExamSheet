using NHibernate;

namespace ExamSheet.Repository.Deanery
{
    public class DeaneryRepository : RepositoryBase<Deanery>
    {
        public DeaneryRepository(ISessionFactory sessionFactory)
            : base(sessionFactory) { }
    }
}