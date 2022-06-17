using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyDigitalMenu.Models
{
    public class Food
    {
        [Key]
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public Double FoodPrice { get; set; }
        public string FoodImageUrl { get; set; }
        public int FoodSortOrder { get; set; }
        public string FoodDescription { get; set; }    
        public DateTime FoodDateAded { get; set; }
        public DateTime FoodDateModified { get; set; }
        public int CategoryId { get; set; }
        public string UserId { get; set; }
        public string RestorantId { get; set; }
        public string Code { get; set; }
    }
}