using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyDigitalMenu.Identity
{ //DbContext işlemleri yapabilmek için DbContext Sınıfından miras alması gerekir.
    //[DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    //[DbConfigurationType(typeof(MySqlEFConfiguration))]
    [DbConfigurationType(typeof(MySql.Data.EntityFramework.MySqlEFConfiguration))]
    public class IdentityDataContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityDataContext() : base("IdentityConnection")
        {

        }
    }
}