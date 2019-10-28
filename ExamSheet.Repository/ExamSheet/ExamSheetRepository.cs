using System;
using System.Collections.Generic;
using NHibernate;

namespace ExamSheet.Repository.ExamSheet
{
    public class ExamSheetRepository : RepositoryBase<ExamSheet>, IRepositoryBase<ExamSheet>
    {
        public ExamSheetRepository(ISessionFactory sessionFactory) 
            : base(sessionFactory) { }

        public override IEnumerable<ExamSheet> FindAll()
        {
            using (var session = sessionFactory.OpenSession())
            {
                return new List<ExamSheet>()
                {
                    new ExamSheet() { Id = Guid.NewGuid().ToString(), TeacherId = "TeacherId", FacultyId = "FacultyId", State = 0, GroupId = "GroupId", SubjectId = "SubjectId", SemesterId = "SemesterId", Ratings = "Ratings" },
                    new ExamSheet() { Id = Guid.NewGuid().ToString(), TeacherId = "TeacherId", FacultyId = "FacultyId", State = 0, GroupId = "GroupId", SubjectId = "SubjectId", SemesterId = "SemesterId", Ratings = "Ratings" },
                    new ExamSheet() { Id = Guid.NewGuid().ToString(), TeacherId = "TeacherId", FacultyId = "FacultyId", State = 0, GroupId = "GroupId", SubjectId = "SubjectId", SemesterId = "SemesterId", Ratings = "Ratings" },
                    new ExamSheet() { Id = Guid.NewGuid().ToString(), TeacherId = "TeacherId", FacultyId = "FacultyId", State = 0, GroupId = "GroupId", SubjectId = "SubjectId", SemesterId = "SemesterId", Ratings = "Ratings" },
                    new ExamSheet() { Id = Guid.NewGuid().ToString(), TeacherId = "TeacherId", FacultyId = "FacultyId", State = 0, GroupId = "GroupId", SubjectId = "SubjectId", SemesterId = "SemesterId", Ratings = "Ratings" },
                    new ExamSheet() { Id = Guid.NewGuid().ToString(), TeacherId = "TeacherId", FacultyId = "FacultyId", State = 0, GroupId = "GroupId", SubjectId = "SubjectId", SemesterId = "SemesterId", Ratings = "Ratings" },
                    new ExamSheet() { Id = Guid.NewGuid().ToString(), TeacherId = "TeacherId", FacultyId = "FacultyId", State = 0, GroupId = "GroupId", SubjectId = "SubjectId", SemesterId = "SemesterId", Ratings = "Ratings" },
                };
            }

        }
    }
}