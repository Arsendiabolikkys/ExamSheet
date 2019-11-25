using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;
using System.Linq;

namespace ExamSheet.Repository.Student
{
    public class StudentRepository : RepositoryBase<Student>
    {
        public StudentRepository(ISessionFactory sessionFactory)
            : base(sessionFactory) { }

        public override IEnumerable<Student> FindAll()
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.Query<Student>();
                return query.OrderBy(x => x.Surname).ToList<Student>();
            }
        }

        public virtual IEnumerable<Student> FindAll(string[] groups, int page, int pageSize)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var criteria = session.CreateCriteria<Student>()
                    .AddOrder(Order.Asc("Surname"))
                    .Add(Restrictions.In("GroupId", groups))
                    .SetFirstResult((page - 1) * pageSize)
                    .SetMaxResults(pageSize);

                return criteria.List<Student>();
            }
        }

        public override IEnumerable<Student> FindAll(int page, int count)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.QueryOver<Student>();
                return query
                    .OrderBy(x => x.Surname)
                    .Asc
                    .Skip((page - 1) * count)
                    .Take(count)
                    .List();
            }
        }

        public virtual int GetTotal(string[] groups)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var criteria = session.CreateCriteria<Student>()
                    .SetProjection(Projections.RowCount())
                    .Add(Restrictions.In("GroupId", groups));

                return criteria.UniqueResult<int>();
            }
        }

        public virtual int GetTotal(string groupId)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var criteria = session.CreateCriteria<Student>()
                    .SetProjection(Projections.RowCount())
                    .Add(Restrictions.Eq("GroupId", groupId));

                return criteria.UniqueResult<int>();
            }
        }

        public IEnumerable<Student> FindGroup(string groupId)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var criteria = session.CreateCriteria<Student>()
                    .Add(Restrictions.Eq("GroupId", groupId));

                return criteria.AddOrder(Order.Asc("Surname")).List<Student>();
            }
        }
    }
}
