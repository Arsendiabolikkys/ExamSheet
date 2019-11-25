using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;
using System.Linq;

namespace ExamSheet.Repository.Group
{
    public class GroupRepository : RepositoryBase<Group>
    {
        public GroupRepository(ISessionFactory sessionFactory)
            : base(sessionFactory) { }

        public override IEnumerable<Group> FindAll()
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.Query<Group>();
                return query.OrderBy(x => x.Name).ToList();
            }
        }

        public override IEnumerable<Group> FindAll(int page, int count)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.QueryOver<Group>();
                return query
                    .OrderBy(x => x.Name)
                    .Asc
                    .Skip((page - 1) * count)
                    .Take(count)
                    .List();
            }
        }

        public virtual IEnumerable<Group> FindAll(string facultyId, int page, int pageSize)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.QueryOver<Group>();
                return query
                    .Where(x => x.FacultyId == facultyId)
                    .OrderBy(x => x.Name)
                    .Asc
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .List();
                //var criteria = session.CreateCriteria<Group>()
                //    .Add(Restrictions.Eq("FacultyId", facultyId))
                //    .AddOrder(Order.Asc("Name"))
                //    .SetFirstResult(page - 1)
                //    .SetMaxResults(pageSize);

                //return criteria.List<Group>();
            }
        }

        public virtual int GetTotal(string facultyId)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var criteria = session.CreateCriteria<Group>()
                    .SetProjection(Projections.RowCount())
                    .Add(Restrictions.Eq("FacultyId", facultyId));

                return criteria.UniqueResult<int>();
            }
        }

        public virtual IEnumerable<Group> GetByIdList(string[] ids)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var criteria = session.CreateCriteria<Group>()
                    .Add(Restrictions.In("Id", ids));

                return criteria.List<Group>();
            }
        }

        public virtual IEnumerable<Group> FindAllForFaculty(string facultyId)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var criteria = session.CreateCriteria<Group>()
                    .Add(Restrictions.Eq("FacultyId", facultyId))
                    .AddOrder(Order.Asc("Name"));
                
                return criteria.List<Group>();
            }
        }
    }
}
