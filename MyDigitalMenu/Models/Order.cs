using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyDigitalMenu.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int TableId { get; set; }
        public int FoodId { get; set; }
        public int CategoryId { get; set; }
        public string FoodName { get; set; }
        public int FoodCount { get; set; }
        public string UserId { get; set; }
        public double Price { get; set; }
        public string RestorantId { get; set; }
        public int State { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdataDate { get; set; }
    }
}