namespace PdfGeneratorApiExample.Templates.Models
{
    public class InvoiceTemplateModel
    {
        public string CustomerName { get; set; }
        public IEnumerable<InvoiceItemModel> Items { get; set; }

    }

    public class InvoiceItemModel
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Total => Quantity * UnitPrice;
    }
}
