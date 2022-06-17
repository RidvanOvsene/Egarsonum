using Microsoft.AspNet.Identity.EntityFramework;
using MyDigitalMenu.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyDigitalMenu.Models
{

    namespace UserIdentity.Models

    {
        public class LoginModel
        {
            [Required]
            public string Username { get; set; }

            [Required]
            public string Password { get; set; }
        }


        public class Register
        {
            [Required]
            public string UserName { get; set; }
            [Required]
            public string Email { get; set; }
            [Required]
            public string Password { get; set; }
            [Required]

            public string CompanyName { get; set; }
            [Required]
            public string Adress { get; set; }
            [Required]
            public string Tel { get; set; }

        }
        public class RoleEditModel
        {
            public IdentityRole Role { get; set; }
            public IEnumerable<ApplicationUser> Members { get; set; }
            public IEnumerable<ApplicationUser> NonMembers { get; set; }
        }

        public class RoleUpdateModel
        {
            [Required]
            public string RoleName { get; set; }
            public string RoleId { get; set; }
            public string[] IdsToAdd { get; set; }
            public string[] IdsToDelete { get; set; }
        }
    }


}