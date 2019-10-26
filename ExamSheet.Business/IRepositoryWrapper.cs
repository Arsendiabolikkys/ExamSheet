using ExamSheet.Business.ExamSheet;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSheet.Business
{
    public interface IRepositoryWrapper
    {
        IExamSheetRepository ExamSheet { get; }
    }
}
