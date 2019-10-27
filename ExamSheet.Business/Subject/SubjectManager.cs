using ExamSheet.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSheet.Business.Subject
{
    public class SubjectManager : BaseManager<SubjectModel>
    {
        public SubjectManager(RepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper) { }

        public override IEnumerable<SubjectModel> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
