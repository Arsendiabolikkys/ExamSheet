using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;

namespace ExamSheet.Repository.Subject
{
    public class SubjectRepository : RepositoryBase<Subject>
    {
        public SubjectRepository(ISessionFactory sessionFactory)
            : base(sessionFactory) { }

        public virtual IEnumerable<Subject> GetByIdList(string[] ids)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var criteria = session.CreateCriteria<Subject>()
                    .Add(Restrictions.In("Id", ids));

                return criteria.List<Subject>();
            }
        }
    }
}
