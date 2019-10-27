using NHibernate;

namespace ExamSheet.Repository.Teacher
{
    public class TeacherRepository : RepositoryBase<Teacher>
    {
        public TeacherRepository(ISessionFactory sessionFactory)
            : base(sessionFactory) { }
    }
}
