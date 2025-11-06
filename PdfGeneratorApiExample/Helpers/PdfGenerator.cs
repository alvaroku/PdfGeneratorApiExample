using iText.Html2pdf;
using iText.IO.Source;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using System.Text;

namespace PdfGeneratorApiExample.Helpers
{
    public class PdfGenerator
    {
        public async Task<byte[]> GeneratePdfAsync(string html)
        {

            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(html));
            ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
            PdfWriter writer = new PdfWriter(byteArrayOutputStream);
            PdfDocument pdfDocument = new PdfDocument(writer);
            pdfDocument.SetDefaultPageSize(PageSize.A4);
            HtmlConverter.ConvertToPdf(stream, pdfDocument);
            pdfDocument.Close();

            return byteArrayOutputStream.ToArray();
        }
    }
}
