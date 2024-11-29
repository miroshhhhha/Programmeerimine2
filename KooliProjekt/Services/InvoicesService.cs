using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class InvoicesService : IInvoicesService
    {
        private readonly ApplicationDbContext _context;

        public InvoicesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Invoice>> GetAllInvoicesAsync()
        {
            try
            {
                return await _context.Invoices.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error fetching invoices", ex);
            }
        }

        public async Task<Invoice> GetInvoiceByIdAsync(int id)
        {
            try
            {
                return await _context.Invoices
                    .FirstOrDefaultAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error fetching invoice with ID {id}", ex);
            }
        }

        public async Task AddInvoiceAsync(Invoice invoice)
        {
            try
            {
                _context.Invoices.Add(invoice);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error adding invoice", ex);
            }
        }

        public async Task UpdateInvoiceAsync(Invoice invoice)
        {
            try
            {
                _context.Invoices.Update(invoice);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error updating invoice", ex);
            }
        }

        public async Task DeleteInvoiceAsync(int id)
        {
            try
            {
                var invoice = await _context.Invoices.FindAsync(id);
                if (invoice != null)
                {
                    _context.Invoices.Remove(invoice);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException($"Invoice with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error deleting invoice with ID {id}", ex);
            }
        }
    }
    public interface IInvoicesService
    {
        Task<List<Invoice>> GetAllInvoicesAsync();
        Task<Invoice> GetInvoiceByIdAsync(int id);
        Task AddInvoiceAsync(Invoice invoice);
        Task UpdateInvoiceAsync(Invoice invoice);
        Task DeleteInvoiceAsync(int id);
    }
}
