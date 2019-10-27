using ExamSheet.Repository;
using System;
using System.Collections.Generic;

namespace ExamSheet.Business.Teacher
{
    public class TeacherManager : BaseManager<TeacherModel>
    {
        public TeacherManager(RepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper) { }

        public override IEnumerable<TeacherModel> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
