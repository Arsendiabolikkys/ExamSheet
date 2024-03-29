﻿using ExamSheet.Business.ExamSheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSheet.Web.Models
{
    public class IndexPageViewModel
    {
        public PageViewModel Page { get; set; }

        public SheetFilterViewModel Filter { get; set; }

        public IList<ExamSheetListViewModel> ExamSheets { get; set; }
    }
}
