using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;
using System.Linq;

namespace ExamSheet.Repository.Subject
{
    public class SubjectRepository : RepositoryBase<Subject>
    {
        public SubjectRepository(ISessionFactory sessionFactory)
            : base(sessionFactory) { }

        public virtual IEnumerable<Subject> FindAllForTeacher(string teacherId)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.CreateSQLQuery("select SubjectId from ExamSheets sh where sh.TeacherId = :teacher");
                var ids = query.SetParameter("teacher", teacherId).List<string>().Distinct().ToArray();

                var criteria = session.CreateCriteria<Subject>()
                    .Add(Restrictions.In("Id", ids));

                return criteria.List<Subject>();
            }
        }

        public virtual IEnumerable<Subject> FindAllForFaculty(string facultyId)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.CreateSQLQuery("select SubjectId from ExamSheets sh where sh.FacultyId = :faculty");
                var ids = query.SetParameter("faculty", facultyId).List<string>().Distinct().ToArray();

                var criteria = session.CreateCriteria<Subject>()
                    .Add(Restrictions.In("Id", ids));

                return criteria.List<Subject>();
            }
        }

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
                    .AddOrder(Order.Asc("Name"))
                    .Add(Restrictions.In("Id", ids));

                return criteria.List<Subject>();
            }
        }
    }
}
