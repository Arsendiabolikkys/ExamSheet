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
                return query.OrderByDescending(x => x.Surname).ToList<Student>();
            }
        }

        public IEnumerable<Student> FindGroup(string groupId)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var criteria = session.CreateCriteria<Student>()
                    .Add(Restrictions.Eq("GroupId", groupId));

                return criteria.AddOrder(Order.Desc("Surname")).List<Student>();
            }
        }
    }
}
