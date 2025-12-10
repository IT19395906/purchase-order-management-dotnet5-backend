using System;
using System.ComponentModel.DataAnnotations;

namespace order_purchase_management.Models
{
    public enum POStatus
    {
        Draft,
        Approved,
        Shipped,
        Completed,
        Cancelled
    }

    public class PurchaseOrder
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string PONumber { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string SupplierName { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Range(0, 999999999)]
        [DataType(DataType.Currency)]
        public decimal TotalAmount { get; set; }

        [Required]
        public POStatus Status { get; set; }
    }
}