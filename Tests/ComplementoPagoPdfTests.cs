using NubeFiscal.PdfGenerator.Models;
using NubeFiscal.PdfGenerator.Services;

namespace NubeFiscal.PdfGenerator.Tests;

public class ComplementoPagoPdfTests
{
    [Fact]
    public void Construir_CfdiComplementoPago20_GeneraPdfValido()
    {
        // Arrange
        var cfdi = new CfdiPdfData
        {
            UUID                  = "B5E9D3C2-1F74-4A08-9E23-7B84C0F16A53",
            RFCEmisor             = "DCN930415TF8",
            NombreEmisor          = "DISTRIBUIDORA CENTRAL DEL NORTE S.A. DE C.V.",
            RegimenFiscalEmisor   = "601",
            LugarExpedicion       = "64000",
            RFCReceptor           = "TLH850603M23",
            NombreReceptor        = "TRANSPORTES Y LOGÍSTICA HERNÁNDEZ HNOS. S.A. DE C.V.",
            RegimenFiscalReceptor = "601",
            CodigoPostalReceptor  = "44100",
            UsoCFDI               = "CP01",
            FechaEmision          = new DateTime(2024, 12, 3, 9, 17, 44),
            FechaTimbrado         = new DateTime(2024, 12, 3, 9, 18, 2),
            TipoComprobante       = "P",
            Moneda                = "XXX",
            Exportacion           = "01",
            SubTotal              = 0m,
            Total                 = 0m,
            NoCertificado         = "00001000000504465028",
            NoCertificadoSAT      = "00001000000403258748",
            SelloCFDI             = "Kj8mXpQ2vNwL5rT9aHcYuE3oBsFdGiZnR7qW1lAeUyV4kPtJ6hMxC0sD+bNvL2mKpR8=",
            SelloSAT              = "Xv9nBqLmP3kT7wRoJ5cEuZaGfHsYiD2eV8yU1tA4rNxW6hFbM0pSjQdKgOlCzI+nBqL=",
            RfcProveedorCertificacion = "SAT970701NN3",
            EstatusSAT            = "Vigente",
            CadenaOriginal        = "||1.1|B5E9D3C2-1F74-4A08-9E23-7B84C0F16A53|2024-12-03T09:18:02|SAT970701NN3|...||",
            Conceptos =
            [
                new ConceptoPdf
                {
                    ClaveProdServ = "84111506", Cantidad = 1m,
                    ClaveUnidad = "ACT", Descripcion = "Pago",
                    ValorUnitario = 0m, Importe = 0m, ObjetoImp = "01"
                }
            ],
            ComplementoPago = new ComplementoPagoPdf
            {
                Version = "2.0",
                Totales = new TotalesPagoPdf
                {
                    TotalTrasladosBaseIVA16     = 340_000.00m,
                    TotalTrasladosImpuestoIVA16 = 54_400.00m,
                    MontoTotalPagos             = 394_400.00m
                },
                Pagos =
                [
                    new PagoPdf
                    {
                        FechaPago    = new DateTime(2024, 11, 29, 12, 0, 0),
                        FormaDePagoP = "03",
                        MonedaP      = "MXN",
                        TipoCambioP  = 1m,
                        Monto        = 212_773.32m,
                        NumOperacion = "0098231174",
                        DoctoRelacionados =
                        [
                            new DoctoRelacionadoPdf
                            {
                                IdDocumento         = "A3F7C2B1-8D45-4E90-BF16-0C92E3A57D84",
                                Serie               = "A",
                                Folio               = "1042",
                                MonedaDR            = "MXN",
                                EquivalenciaDR      = 1m,
                                NumParcialidad      = 1,
                                ImpSaldoAnt         = 212_773.32m,
                                ImpPagado           = 212_773.32m,
                                ImpSaldoInsoluto    = 0m,
                                ObjetoImpDR         = "02",
                                TrasladosDR =
                                [
                                    new ImpuestoPagoDetalladoPdf
                                    {
                                        Base        = 183_701.00m,
                                        Impuesto    = "002",
                                        TipoFactor  = "Tasa",
                                        TasaOCuota  = 0.160000m,
                                        Importe     = 29_392.16m
                                    }
                                ]
                            }
                        ],
                        TrasladosP =
                        [
                            new ImpuestoPagoDetalladoPdf
                            {
                                Base       = 183_701.00m,
                                Impuesto   = "002",
                                TipoFactor = "Tasa",
                                TasaOCuota = 0.160000m,
                                Importe    = 29_392.16m
                            }
                        ]
                    }
                ]
            }
        };

        // Act
        var pdfBytes = PdfBuilder.Construir(cfdi);

        // Assert
        PdfTestHelpers.AssertValidPdf(pdfBytes);
    }
}
