using NubeFiscal.PdfGenerator.Models;
using NubeFiscal.PdfGenerator.Services;

namespace NubeFiscal.PdfGenerator.Tests;

public class CanceladoPdfTests
{
    [Fact]
    public void Construir_CfdiCancelado_GeneraPdfValido()
    {
        var cfdi = new CfdiPdfData
        {
            UUID                = "12345678-1234-1234-1234-1234567890AB",
            RFCEmisor           = "XAXX010101000",
            NombreEmisor        = "EMPRESA PRUEBA SA DE CV",
            RegimenFiscalEmisor = "601",
            LugarExpedicion     = "06000",
            RFCReceptor         = "XEXX010101000",
            NombreReceptor      = "PUBLICO EN GENERAL",
            RegimenFiscalReceptor = "616",
            CodigoPostalReceptor = "06000",
            UsoCFDI             = "S01",
            FechaEmision        = new DateTime(2024, 5, 1, 10, 0, 0),
            FechaTimbrado       = new DateTime(2024, 5, 1, 10, 5, 0),
            FechaCancelacion    = new DateTime(2024, 5, 10, 12, 0, 0),
            EstatusSAT          = "Cancelado",
            TipoComprobante     = "I",
            Moneda              = "MXN",
            Total               = 1000m,
            SubTotal            = 1000m,
            Conceptos =
            [
                new ConceptoPdf
                {
                    ClaveProdServ = "01010101", Cantidad = 1m,
                    ClaveUnidad = "ACT", Descripcion = "Servicio cancelado",
                    ValorUnitario = 1000m, Importe = 1000m, ObjetoImp = "01"
                }
            ]
        };

        var pdfBytes = PdfBuilder.Construir(cfdi);

        PdfTestHelpers.AssertValidPdf(pdfBytes);
    }
}
