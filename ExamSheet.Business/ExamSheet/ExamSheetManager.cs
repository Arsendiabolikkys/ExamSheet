using ExamSheet.Repository;
using System.Collections.Generic;

namespace ExamSheet.Business.ExamSheet
{
    public class ExamSheetManager : BaseManager<ExamSheetModel>
    {
        public ExamSheetManager(RepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper) { }

        public override IEnumerable<ExamSheetModel> FindAll()
        {
            //TODO: add CacheManager layer
            //TODO: mock
            //TODO: add call to Data project (get from DB)
            var examSheets = repositoryWrapper.ExamSheet.FindAll();
            //MAPPING
            return new List<ExamSheetModel>();
        }
    }
}