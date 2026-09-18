using NubeFiscal.PdfGenerator.Models;
using NubeFiscal.PdfGenerator.Services;

namespace NubeFiscal.PdfGenerator.Tests;

public class IngresoPdfTests
{
    [Fact]
    public void Construir_CfdiIngresoMultiConcepto_GeneraPdfValido()
    {
        // Arrange
        var cfdi = new CfdiPdfData
        {
            UUID                      = "A3F7C2B1-8D45-4E90-BF16-0C92E3A57D84",
            RFCEmisor                 = "DCN930415TF8",
            NombreEmisor              = "DISTRIBUIDORA CENTRAL DEL NORTE S.A. DE C.V.",
            RegimenFiscalEmisor       = "601",
            LugarExpedicion           = "64000",
            RFCReceptor               = "TLH850603M23",
            NombreReceptor            = "TRANSPORTES Y LOGÍSTICA HERNÁNDEZ HNOS. S.A. DE C.V.",
            RegimenFiscalReceptor     = "601",
            CodigoPostalReceptor      = "44100",
            UsoCFDI                   = "G03",
            FechaEmision              = new DateTime(2024, 11, 8, 14, 32, 55),
            FechaTimbrado             = new DateTime(2024, 11, 8, 14, 33, 18),
            TipoComprobante           = "I",
            Moneda                    = "MXN",
            FormaPago                 = "03",
            MetodoPago                = "PPD",
            Exportacion               = "01",
            Serie                     = "A",
            Folio                     = "1042",
            SubTotal                  = 187_450.00m,
            Descuento                 = 3_749.00m,
            TotalImpuestosTrasladados = 29_072.32m,
            Total                     = 212_773.32m,
            NoCertificado             = "00001000000504465028",
            NoCertificadoSAT          = "00001000000403258748",
            SelloCFDI                 = "Kj8mXpQ2vNwL5rT9aHcYuE3oBsFdGiZnR7qW1lAeUyV4kPtJ6hMxC0sD+bNvL2mKpR8=",
            SelloSAT                  = "Xv9nBqLmP3kT7wRoJ5cEuZaGfHsYiD2eV8yU1tA4rNxW6hFbM0pSjQdKgOlCzI+nBqL=",
            RfcProveedorCertificacion = "SAT970701NN3",
            EstatusSAT                = "Vigente",
            CadenaOriginal            = "||1.1|A3F7C2B1-8D45-4E90-BF16-0C92E3A57D84|2024-11-08T14:33:18|SAT970701NN3|...||",
            Conceptos =
            [
                new ConceptoPdf
                {
                    ClaveProdServ = "15101514", NoIdentificacion = "DIES-001",
                    Cantidad = 5000m, ClaveUnidad = "LTR", Unidad = "Litro",
                    Descripcion = "Diesel automotriz (B5)",
                    ValorUnitario = 22.48m, Importe = 112_400m,
                    ObjetoImp = "02",
                    Traslados = [new TrasladoPdf { Impuesto = "002", TasaOCuota = "0.160000", Importe = 17_984m }]
                },
                new ConceptoPdf
                {
                    ClaveProdServ = "15121806", NoIdentificacion = "LUB-010",
                    Cantidad = 80m, ClaveUnidad = "LTR", Unidad = "Litro",
                    Descripcion = "Aceite de motor 15W-40",
                    ValorUnitario = 95m, Importe = 7_600m,
                    Descuento = 760m,
                    ObjetoImp = "02",
                    Traslados = [new TrasladoPdf { Impuesto = "002", TasaOCuota = "0.160000", Importe = 1_094.40m }],
                    Retenciones = [new RetencionPdf { Impuesto = "001", Importe = 760m }]
                }
            ]
        };

        // Act
        var pdfBytes = PdfBuilder.Construir(cfdi);

        // Assert
        PdfTestHelpers.AssertValidPdf(pdfBytes);
    }

    [Fact]
    public void Construir_DesdeXmlRealIngreso_GeneraPdfValido()
    {
        // Arrange
        var xmlPath = Path.Combine(AppContext.BaseDirectory, "TestData", "ingreso_sample.xml");
        Assert.True(File.Exists(xmlPath), $"No se encontró el archivo de prueba: {xmlPath}");

        var xml = File.ReadAllText(xmlPath);
        var cfdi = CfdiXmlParser.FromXml(xml);

        Assert.Equal("I", cfdi.TipoComprobante);
        Assert.NotEmpty(cfdi.Conceptos);
        Assert.NotNull(cfdi.Total);

        // Act
        var pdfBytes = PdfBuilder.Construir(cfdi);

        // Assert
        PdfTestHelpers.AssertValidPdf(pdfBytes);
    }
}
