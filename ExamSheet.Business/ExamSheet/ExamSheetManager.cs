using ExamSheet.Business.ExamSheet;
using System;
using System.Collections.Generic;

namespace ExamSheet.Business.ExamSheet
{
    public class ExamSheetManager : BaseManager<ExamSheetModel>, IExamSheetManager
    {
        public ExamSheetManager(IRepositoryWrapper repositoryWrapper)
            : base(repositoryWrapper) { }

        public IEnumerable<ExamSheetModel> FindAll()
        {
            //TODO: add CacheManager layer
            //TODO: mock
            //TODO: add call to Data project (get from DB)
            return repositoryWrapper.ExamSheet.FindAll();
        }
    }
}