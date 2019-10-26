using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSheet.Business.ExamSheet
{
    public interface IExamSheetManager
    {
        IEnumerable<ExamSheet> GetExamSheets();
    }
}