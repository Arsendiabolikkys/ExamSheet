using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;
using System.Linq;

namespace ExamSheet.Repository.Faculty
{
    public class FacultyRepository : RepositoryBase<Faculty>
    {
        public FacultyRepository(ISessionFactory sessionFactory)
            : base(sessionFactory) { }

        public virtual IEnumerable<Faculty> FindAllForTeacher(string teacherId)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.CreateSQLQuery("select FacultyId from ExamSheets sh where sh.TeacherId = :teacher");
                var ids = query.SetParameter("teacher", teacherId).List<string>().Distinct().ToArray();

                var criteria = session.CreateCriteria<Faculty>()
                    .Add(Restrictions.In("Id", ids));

                return criteria.List<Faculty>();
            }
        }
    }
}
