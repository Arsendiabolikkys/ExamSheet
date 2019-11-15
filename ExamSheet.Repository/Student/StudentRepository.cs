using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;

namespace ExamSheet.Repository.Student
{
    public class StudentRepository : RepositoryBase<Student>
    {
        public StudentRepository(ISessionFactory sessionFactory)
            : base(sessionFactory) { }

        public IEnumerable<Student> FindGroup(string groupId)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var criteria = session.CreateCriteria<Student>()
                    .Add(Restrictions.Eq("GroupId", groupId));

                return criteria.List<Student>();
            }
        }
    }
}
