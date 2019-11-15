using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;

namespace ExamSheet.Repository.Group
{
    public class GroupRepository : RepositoryBase<Group>
    {
        public GroupRepository(ISessionFactory sessionFactory)
            : base(sessionFactory) { }

        public virtual IEnumerable<Group> FindAllForFaculty(string facultyId)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var criteria = session.CreateCriteria<Group>()
                    .Add(Restrictions.Eq("FacultyId", facultyId));
                
                return criteria.List<Group>();
            }
        }
    }
}
