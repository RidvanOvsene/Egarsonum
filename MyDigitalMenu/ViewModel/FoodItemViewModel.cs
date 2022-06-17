using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDigitalMenu.ViewModel
{
    public class FoodItemViewModel
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public string Text { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string UserId { get; set; }
        public string RestonrantId { get; set; }
    }
}