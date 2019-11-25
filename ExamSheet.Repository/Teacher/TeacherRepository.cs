using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;

namespace ExamSheet.Repository.Teacher
{
    public class TeacherRepository : RepositoryBase<Teacher>
    {
        public TeacherRepository(ISessionFactory sessionFactory)
            : base(sessionFactory) { }

        public override IEnumerable<Teacher> FindAll(int page, int count)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.QueryOver<Teacher>();
                return query
                    .OrderBy(x => x.Surname)
                    .Asc
                    .Skip((page - 1) * count)
                    .Take(count)
                    .List();
            }
        }

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
