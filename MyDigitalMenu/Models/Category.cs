using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyDigitalMenu.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int CategorySortOrder { get; set; }
        public DateTime CategoryDateAded { get; set; }
        public DateTime CategoryDateModified { get; set; }
        public string UserId { get; set; }
        public string RestorantId { get; set; }
        public  string ImagePath { get; set; }
        public string Code { get; set; }
    }
}