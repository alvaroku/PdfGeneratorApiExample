using Microsoft.AspNetCore.Mvc;
using PdfGeneratorApiExample.Helpers;
using PdfGeneratorApiExample.Templates;
using PdfGeneratorApiExample.Templates.Models;

namespace PdfGeneratorApiExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController(RazorRenderer renderer, PdfGenerator pdfGenerator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // Generamos datos ficticios en un Modelo
            InvoiceTemplateModel model = new InvoiceTemplateModel
            {
                CustomerName = "John Doe",
                Items = [
                                new InvoiceItemModel
                        {
                            Description = "Item 1",
                            Quantity = 2,
                            UnitPrice = 10.0,

                        },
                        new InvoiceItemModel
                        {
                            Description = "Item 2",
                            Quantity = 1,
                            UnitPrice = 35.0,
                        },
                        new InvoiceItemModel
                        {
                            Description = "Item 3",
                            Quantity = 2,
                            UnitPrice = 10.0,
                        },
                        new InvoiceItemModel
                        {
                            Description = "Item 4",
                            Quantity = 3,
                            UnitPrice = 3.0,
                        },
        ]
            };

            // Renderizamos el componente de Razor a un String pasando los datos del Modelo
            var html = await renderer.RenderComponentAsync<InvoiceTemplate>(new Dictionary<string, object?>
            {
                { "Model", model }
            });

            // Ese mismo String, que contiene todo el HTML, lo convertimos en el documento PDF
            var pdfBytes = await pdfGenerator.GeneratePdfAsync(html);

            // Guardamos el archivo PDF de forma local
            System.IO.File.WriteAllBytes("invoice.pdf", pdfBytes);

            // La api descarga el PDF en el navegador
            return File(pdfBytes, "application/pdf", "invoice.pdf");
        }
    }
}
