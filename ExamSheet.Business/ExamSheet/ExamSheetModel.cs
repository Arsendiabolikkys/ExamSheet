using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSheet.Business.ExamSheet
{
    public class ExamSheetModel
    {
        public virtual string Id { get; set; }

        public virtual ExamSheetState State { get; set; }

        public virtual DateTime? OpenDate { get; set; }

        public virtual DateTime? CloseDate { get; set; }

        //TODO: better structure
        public virtual string Ratings { get; set; }
    }

    public enum ExamSheetState
    {
        New,
        Open,
        Closed,
        Archived
    }
}