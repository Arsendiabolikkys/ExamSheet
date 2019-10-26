using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSheet.Data.ExamSheet
{
    public class ExamSheetRepository : RepositoryBase<ExamSheet>, IExamSheetRepository
    {
        public ExamSheetRepository(RepositoryContext repositoryContext)
            : base(repositoryContext) { }

        IEnumerable<ExamSheet> IRepositoryBase<Models.ExamSheet>.FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
