using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;

namespace ExamSheet.Repository.ExamSheet
{
    public class ExamSheetRepository : RepositoryBase<ExamSheet>, IRepositoryBase<ExamSheet>
    {
        public ExamSheetRepository(ISessionFactory sessionFactory) 
            : base(sessionFactory) { }

        public virtual ExamSheet Get(string groupId, string teacherId, string subjectId, short year, short semester)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var criteria = session.CreateCriteria<ExamSheet>()
                    .Add(Restrictions.Eq("GroupId", groupId))
                    .Add(Restrictions.Eq("TeacherId", teacherId))
                    .Add(Restrictions.Eq("SubjectId", subjectId))
                    .Add(Restrictions.Eq("Year", year))
                    .Add(Restrictions.Eq("Semester", semester));

                return criteria.UniqueResult<ExamSheet>();
            }
        }

        public virtual IEnumerable<ExamSheet> FindClosedForTeacher(string teacherId, short state)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var criteria = session.CreateCriteria<ExamSheet>()
                    .Add(Restrictions.Eq("TeacherId", teacherId))
                    .Add(Restrictions.Eq("State", state));

                return criteria.List<ExamSheet>();
            }
        }

        public virtual IEnumerable<ExamSheet> FindAllForTeacher(string teacherId)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var criteria = session.CreateCriteria<ExamSheet>()
                    .Add(Restrictions.Eq("TeacherId", teacherId));

                return criteria.List<ExamSheet>();
            }
        }

        public virtual IEnumerable<ExamSheet> FindAllForFaculty(string facultyId)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var criteria = session.CreateCriteria<ExamSheet>()
                    .Add(Restrictions.Eq("FacultyId", facultyId));
                
                return criteria.List<ExamSheet>();
            }
        }
    }
}