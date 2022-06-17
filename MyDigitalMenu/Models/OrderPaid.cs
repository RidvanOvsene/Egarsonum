using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyDigitalMenu.Models
{
    public class OrderPaid
    {
        [Key]
        public int OrderId { get; set; }
        public int TableId { get; set; }
        public string UserId { get; set; }
        public double TotalPrice { get; set; }
        public string RestorantId { get; set; }
        public string PaymentType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdataDate { get; set; }
    }
}