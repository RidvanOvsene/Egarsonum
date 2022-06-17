using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyDigitalMenu.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        [DataType(DataType.Password)]
        [MaxLength(500)]
        public string Password { get; set; }
        [MaxLength(500)]
        public string Email { get; set; }
        [MaxLength(500)]
        public string UserName { get; set; }
        [MaxLength(500)]
        public string CompanyName { get; set; }
        [MaxLength(500)]
        public string Role { get; set; }
        [MaxLength(150)]
        public string ProfileImgPath { get; set; }    
     
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [MaxLength(50)]
        public string CreateedBy { get; set; }
        public bool IsActive { get; set; }
    }
}