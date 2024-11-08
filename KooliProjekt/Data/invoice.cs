using Microsoft.AspNetCore.Identity;

namespace KooliProjekt.Data
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Price { get; set; }
        public static int Event_Id { get; set; }
        public Boolean Is_Paid { get; set; }
        public IdentityUser Customer { get; set; }
        public string Customer_Id { get; set; }

        // Navigational property
        public IList<InvoiceLine> Lines { get; set; }

        public Invoice()
        {
            Lines = new List<InvoiceLine>();
        }


    }
}
