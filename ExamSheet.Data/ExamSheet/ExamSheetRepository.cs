using ExamSheet.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSheet.Data.ExamSheet
{
    public class ExamSheetRepository : RepositoryBase<Faculty>, IExamSheetRepository
    {
        public ExamSheetRepository(RepositoryContext repositoryContext)
            : base(repositoryContext) { }
    }
}
