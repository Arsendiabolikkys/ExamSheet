using ExamSheet.Repository;
using System;
using System.Collections.Generic;

namespace ExamSheet.Business.Deanery
{
    public class DeaneryManager : BaseManager<DeaneryModel>
    {
        public DeaneryManager(RepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper) { }

        public override IEnumerable<DeaneryModel> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
