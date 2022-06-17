using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDigitalMenu.ViewModel
{
    public class TableStatusViewModel
    {
        public int TableId { get; set; }
        public int SectionId { get; set; }
        public string TableName { get; set; }
        public string SectionName { get; set; }
        public int OrderStatus { get; set; }
        public int OrderId { get; set; }
        public int TableSortOrder { get; set; }
    }
}