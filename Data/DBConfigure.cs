using Microsoft.EntityFrameworkCore;
using order_purchase_management.Models;

namespace order_purchase_management.Data
{
    public class DBConfigure : DbContext
    {
        public DBConfigure(DbContextOptions<DBConfigure> options) : base(options)
        {
        }

        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
    }
}