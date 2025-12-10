using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using order_purchase_management.Data;
using order_purchase_management.Models;
using order_purchase_management.DTOs;

namespace order_purchase_management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly DBConfigure _context;

        public PurchaseOrdersController(DBConfigure context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders(string supplier, string status, int page = 1, int pageSize = 10)
        {
            var query = _context.PurchaseOrders.AsQueryable();

            if (!string.IsNullOrEmpty(supplier))
            {
                query = query.Where(p => p.SupplierName.Contains(supplier));
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(p => p.Status == status);
            }

            var total = await query.CountAsync();

            var purchaseOrders = await query.OrderByDescending(p => p.OrderDate).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return Ok(new { total, data = purchaseOrders });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var pOrder = await _context.PurchaseOrders.FindAsync(id);
            if (pOrder == null)
            {
                return NotFound();
            }

            return Ok(pOrder);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(PurchaseOrderDto dto)
        {
            var pOrder = new PurchaseOrder
            {
                PONumber = dto.PONumber,
                Description = dto.Description,
                SupplierName = dto.SupplierName,
                OrderDate = dto.OrderDate,
                TotalAmount = decimal.Round(dto.TotalAmount, 2),
                Status = dto.Status
            };

            _context.PurchaseOrders.Add(pOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrderById), new { id = pOrder.Id }, pOrder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, PurchaseOrderDto dto)
        {
            var pOrder = await _context.PurchaseOrders.FindAsync(id);
            if (pOrder == null)
            {
                return NotFound();
            }

            pOrder.PONumber = dto.PONumber;
            pOrder.Description = dto.Description;
            pOrder.SupplierName = dto.SupplierName;
            pOrder.OrderDate = dto.OrderDate;
            pOrder.TotalAmount = decimal.Round(dto.TotalAmount, 2);
            pOrder.Status = dto.Status;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var pOrder = await _context.PurchaseOrders.FindAsync(id);
            if (pOrder == null)
            {
                return NotFound();
            }

            _context.PurchaseOrders.Remove(pOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}