using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;
using System.Linq;

namespace ExamSheet.Repository.Teacher
{
    public class TeacherRepository : RepositoryBase<Teacher>
    {
        public TeacherRepository(ISessionFactory sessionFactory)
            : base(sessionFactory) { }

        public virtual IEnumerable<Teacher> FindAllForFaculty(string facultyId)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var query = session.CreateSQLQuery("select TeacherId from ExamSheets sh where sh.FacultyId = :faculty");
                var ids = query.SetParameter("faculty", facultyId).List<string>().Distinct().ToArray();

                var criteria = session.CreateCriteria<Teacher>()
                    .Add(Restrictions.In("Id", ids));

                return criteria.List<Teacher>();
            }
        }

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
