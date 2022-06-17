using Microsoft.AspNet.Identity;
using MyDigitalMenu.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDigitalMenu.Controllers
{
    internal class userManager
    {
        public static CustomPasswordValidator PasswordValidator { get; internal set; }
        public static UserValidator<ApplicationUser> UserValidator { get; internal set; }
    }
}