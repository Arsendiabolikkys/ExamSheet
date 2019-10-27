using ExamSheet.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSheet.Business.Semester
{
    public class SemesterManager : BaseManager<SemesterModel>
    {
        public SemesterManager(RepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper) { }

        public override IEnumerable<SemesterModel> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
