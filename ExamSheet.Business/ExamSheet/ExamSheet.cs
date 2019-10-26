using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSheet.Business.ExamSheet
{
    public class ExamSheet
    {
        public string Id { get; set; }

        public ExamSheetState State { get; set; }

        public DateTime? OpenDate { get; set; }

        public DateTime? CloseDate { get; set; }

        //TODO: XML
        public string Ratings { get; set; }
    }

    public enum ExamSheetState
    {
        New,
        Open,
        Closed,
        Archived
    }
}