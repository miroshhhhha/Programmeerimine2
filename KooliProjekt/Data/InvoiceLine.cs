namespace KooliProjekt.Data
{
    public class InvoiceLine
    {
        public required int Id { get; set; }
        public string? InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}