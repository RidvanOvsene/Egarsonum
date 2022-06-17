using MyDigitalMenu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDigitalMenu.ViewModel
{
    public class MainModel
    {
        public List<Category> Category { get; set; }
        public List<Language> Language { get; set; }
    }


    public class Language
    {
        public string Code { get; set; }
        public string Text { get; set; }
        public int Culture { get; set; }
    }

}
