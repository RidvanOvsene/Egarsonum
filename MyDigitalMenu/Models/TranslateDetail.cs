using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyDigitalMenu.Models
{
    public class TranslateDetail
    {
        [Key]
        public int Id { get; set; }
        public int TranslateId { get; set; }
        public string Text { get; set; }
        public int Culture { get; set; }
    }
}