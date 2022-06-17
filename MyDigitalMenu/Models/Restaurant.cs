using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyDigitalMenu.Models
{
    public class Restaurant
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool IsOrder { get; set; }

    }
}