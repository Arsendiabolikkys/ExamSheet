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

        public virtual IEnumerable<ExamSheet> Get(string facultyId, string subjectId)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var criteria = session.CreateCriteria<ExamSheet>()
                    .Add(Restrictions.Eq("FacultyId", facultyId))
                    .Add(Restrictions.Eq("SubjectId", subjectId));

                return criteria.List<ExamSheet>();
            }
        }

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

        public virtual IEnumerable<ExamSheet> FindForTeacher(string teacherId, short state)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var criteria = session.CreateCriteria<ExamSheet>()
                    .Add(Restrictions.Eq("TeacherId", teacherId))
                    .Add(Restrictions.Eq("State", state));

                return criteria.List<ExamSheet>();
            }
        }

        public virtual IEnumerable<ExamSheet> FindForFaculty(string facultyId, short state)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var criteria = session.CreateCriteria<ExamSheet>()
                    .Add(Restrictions.Eq("FacultyId", facultyId))
                    .Add(Restrictions.Eq("State", state));

                return criteria.List<ExamSheet>();
            }
        }

        public virtual int GetTotalForTeacher(string teacherId)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var criteria = session.CreateCriteria<ExamSheet>()
                    .SetProjection(Projections.RowCount())
                    .Add(Restrictions.Eq("TeacherId", teacherId))
                        .Add(Restrictions.Disjunction()
                            .Add(Restrictions.Eq("State", (short)1))
                            .Add(Restrictions.Eq("State", (short)2))
                            .Add(Restrictions.Conjunction().Add(Restrictions.Eq("State", (short)0))
                                                           .Add(Restrictions.Le("OpenDate", DateTime.Now)))
                        );
                
                return criteria.UniqueResult<int>();
            }
        }

        public virtual int GetTotalForFaculty(string facultyId)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var criteria = session.CreateCriteria<ExamSheet>()
                    .SetProjection(Projections.RowCount())
                    .Add(Restrictions.Eq("FacultyId", facultyId));

                return criteria.UniqueResult<int>();
            }
        }

        public virtual IEnumerable<ExamSheet> FindAllForTeacher(string teacherId, int page, int pageSize)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var criteria = session.CreateCriteria<ExamSheet>()
                    .Add(Restrictions.Eq("TeacherId", teacherId))
                    .Add(Restrictions.Disjunction()
                        .Add(Restrictions.Eq("State", (short)1))
                        .Add(Restrictions.Eq("State", (short)2))
                        .Add(Restrictions.Conjunction().Add(Restrictions.Eq("State", (short)0))
                                                       .Add(Restrictions.Le("OpenDate", DateTime.Now)))
                        )
                     .SetFirstResult(page - 1)
                     .SetMaxResults(pageSize);

                return criteria.List<ExamSheet>();
            }
        }

        public virtual IEnumerable<ExamSheet> FindAllForFaculty(string facultyId, int page, int pageSize)
        {
            using (var session = sessionFactory.OpenSession())
            {
                var criteria = session.CreateCriteria<ExamSheet>()
                    .Add(Restrictions.Eq("FacultyId", facultyId))
                    .SetFirstResult(page - 1)
                    .SetMaxResults(pageSize);
                
                return criteria.List<ExamSheet>();
            }
        }
    }
}