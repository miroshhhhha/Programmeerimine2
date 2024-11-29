using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class InvoiceLineService : IInvoiceLineService
    {
        private readonly ApplicationDbContext _context;

        public InvoiceLineService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<InvoiceLine>> GetInvoicesLineAsync(int invoiceId)
        {
            return await _context.InvoicesLine
                .Where(il => il.Id == invoiceId)
                .ToListAsync();
        }

        public async Task<InvoiceLine> GetInvoiceLineByIdAsync(int id)
        {
            return await _context.InvoicesLine
                .FirstOrDefaultAsync(il => il.Id == id);
        }

        public async Task AddInvoiceLineAsync(InvoiceLine invoiceLine)
        {
            _context.InvoicesLine.Add(invoiceLine);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateInvoiceLineAsync(InvoiceLine invoiceLine)
        {
            _context.InvoicesLine.Update(invoiceLine);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInvoiceLineAsync(int id)
        {
            var invoiceLine = await _context.InvoicesLine.FindAsync(id);
            if (invoiceLine != null)
            {
                _context.InvoicesLine.Remove(invoiceLine);
                await _context.SaveChangesAsync();
            }
        }
    }
    public interface IInvoiceLineService
    {
        Task<List<InvoiceLine>> GetInvoicesLineAsync(int invoiceId);
        Task<InvoiceLine> GetInvoiceLineByIdAsync(int id);
        Task AddInvoiceLineAsync(InvoiceLine invoiceLine);
        Task UpdateInvoiceLineAsync(InvoiceLine invoiceLine);
        Task DeleteInvoiceLineAsync(int id);
    }
}
