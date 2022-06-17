using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyDigitalMenu.Models
{
    public class Translate
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
    }
}