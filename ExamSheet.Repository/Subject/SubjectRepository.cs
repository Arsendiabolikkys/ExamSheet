using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;

namespace ExamSheet.Repository.Subject
{
    public class SubjectRepository : RepositoryBase<Subject>
    {
        public SubjectRepository(ISessionFactory sessionFactory)
            : base(sessionFactory) { }

        public override IEnumerable<Subject> FindAll(int page, int count)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.QueryOver<Subject>();
                return query
                    .OrderBy(x => x.Name)
                    .Asc
                    .Skip((page - 1) * count)
                    .Take(count)
                    .List();
            }
        }

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
