using ExamSheet.Data.ExamSheet;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSheet.Data
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext repositoryContext;
        private IExamSheetRepository examSheet;

        public IExamSheetRepository ExamSheet
        {
            get
            {
                if (examSheet == null)
                {
                    examSheet = new ExamSheetRepository(repositoryContext);
                }
                return examSheet;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }
    }
}
