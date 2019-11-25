using System;
using System.Collections.Generic;

namespace ExamSheet.Web.Models
{
    public class ItemsViewModel
    {
        public PageViewModel Page { get; set; }

        public IList<IItemViewModel> Items { get; set; }
    }
}
