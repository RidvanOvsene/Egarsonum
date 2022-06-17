using MyDigitalMenu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDigitalMenu.ViewModel
{
    public class FoodViewModel
    {
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
        public List<FoodItems> FoodItems { get; set; }
        public string FoodItemText { get; set; }
        public string Code { get; set; }

    }
    public  class FoodViewMainModel
    {
        public List<Language> Language { get; set; }
        public List<FoodViewModel> FoodViewModel { get; set; }

    }
}