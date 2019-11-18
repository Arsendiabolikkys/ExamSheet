using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;

namespace ExamSheet.Repository.Teacher
{
    public class TeacherRepository : RepositoryBase<Teacher>
    {
        public TeacherRepository(ISessionFactory sessionFactory)
            : base(sessionFactory) { }

        public virtual IEnumerable<Teacher> GetByIdList(string[] ids)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var criteria = session.CreateCriteria<Teacher>()
                    .Add(Restrictions.In("Id", ids));

                return criteria.List<Teacher>();
            }
        }
    }
}
