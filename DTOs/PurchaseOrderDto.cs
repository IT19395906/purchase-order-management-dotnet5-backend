using System;
using order_purchase_management.Models;

namespace order_purchase_management.DTOs
{
    public class PurchaseOrderDto
    {
        public string PONumber { get; set; }
        public string Description { get; set; }
        public string SupplierName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public POStatus Status { get; set; }
    }
}