using NHibernate;

namespace ExamSheet.Repository.Student
{
    public class StudentRepository : RepositoryBase<Student>
    {
        public StudentRepository(ISessionFactory sessionFactory)
            : base(sessionFactory) { }
    }
}
