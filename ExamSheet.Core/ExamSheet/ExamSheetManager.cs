using ExamSheet.Business.ExamSheet;
using System;
using System.Collections.Generic;
 
namespace ExamSheet.Core.ExamSheet
{
    public class ExamSheetManager : BaseManager<ExamSheet.Data.Models.ExamSheet>, IExamSheetManager
    {
        public IEnumerable<Business.ExamSheet.ExamSheet> FindAll()
        {
            //TODO: add CacheManager layer
            //TODO: mock
            //TODO: add call to Data project (get from DB)
            return new List<Business.ExamSheet.ExamSheet>()
            {

                new Business.ExamSheet.ExamSheet(){Id = Guid.NewGuid().ToString()}
            };
        }
    }
}