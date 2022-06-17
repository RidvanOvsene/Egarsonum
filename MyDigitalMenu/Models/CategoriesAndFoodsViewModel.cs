
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDigitalMenu.Models
{
    public class CategoriesAndFoodsViewModel
    {
        public int CategoryId { get; set; }
        public String CategoryName { get; set; }
        public String CategoryImageUrl { get; set; }
        public int CategorySortOrder { get; set; }
        public int FoodId { get; set; }
        public String FoodName { get; set; }
        public Double FoodPrice { get; set; }
        public String FoodImageUrl { get; set; }
        public int FoodSortOrder { get; set; }
        public String FoodIngredients1 { get; set; }
        public String FoodIngredients2 { get; set; }
        public String FoodIngredients3 { get; set; }
        public String FoodDescription { get; set; }
    }
}