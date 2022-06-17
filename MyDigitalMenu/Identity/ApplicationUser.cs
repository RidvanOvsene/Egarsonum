using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDigitalMenu.Identity
{
    public class ApplicationUser:IdentityUser
    {

        // Eklenmek istenen Alan buradan eklenir
        //public string ProvinceCode { get; set; }
        public string CompanyName { get; set; }
        public string Adress { get; set; }
        public string Tel { get; set; }
        public string RestorantId { get; set; }
    }
}