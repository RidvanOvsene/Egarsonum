using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDigitalMenu.ViewModel
{
    public class DinnerTableViewModel
    {
        public int TableId { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public string TableName { get; set; }
        public int TableSortOrder { get; set; }
        public string UserId { get; set; }
        public string RestorantId { get; set; }
        public bool State { get; set; }
    }
}