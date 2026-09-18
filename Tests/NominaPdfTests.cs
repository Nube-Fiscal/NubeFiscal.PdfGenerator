using NubeFiscal.PdfGenerator.Models;
using NubeFiscal.PdfGenerator.Services;

namespace NubeFiscal.PdfGenerator.Tests;

public class NominaPdfTests
{
    [Fact]
    public void Construir_CfdiNominaModelo_GeneraPdfValido()
    {
        // Arrange
        var cfdi = new CfdiPdfData
        {
            UUID                      = "E7B0D4A3-9C52-4F86-2E47-5D18F3C09A72",
            RFCEmisor                 = "TEC840601NX3",
            NombreEmisor              = "SOLUCIONES TECNOLÓGICAS NEXO S.A. DE C.V.",
            RegimenFiscalEmisor       = "601",
            LugarExpedicion           = "45010",
            RFCReceptor               = "MARG870924QT5",
            NombreReceptor            = "GARCÍA RAMOS MARIANA",
            RegimenFiscalReceptor     = "605",
            CodigoPostalReceptor      = "45010",
            UsoCFDI                   = "CN01",
            FechaEmision              = new DateTime(2024, 11, 15, 23, 59, 59),
            FechaTimbrado             = new DateTime(2024, 11, 16, 0, 0, 24),
            TipoComprobante           = "N",
            Moneda                    = "MXN",
            FormaPago                 = "03",
            MetodoPago                = "PUE",
            Exportacion               = "01",
            SubTotal                  = 21_200.00m,
            Total                     = 21_200.00m,
            NoCertificado             = "00001000000504465028",
            NoCertificadoSAT          = "00001000000403258748",
            SelloCFDI                 = "Kj8mXpQ2vNwL5rT9aHcYuE3oBsFdGiZnR7qW1lAeUyV4kPtJ6hMxC0sD+bNvL2mKpR8=",
            SelloSAT                  = "Xv9nBqLmP3kT7wRoJ5cEuZaGfHsYiD2eV8yU1tA4rNxW6hFbM0pSjQdKgOlCzI+nBqL=",
            RfcProveedorCertificacion = "SAT970701NN3",
            EstatusSAT                = "Vigente",
            CadenaOriginal            = "||1.1|E7B0D4A3-9C52-4F86-2E47-5D18F3C09A72|2024-11-16T00:00:24|SAT970701NN3|...||",
            Conceptos =
            [
                new ConceptoPdf
                {
                    ClaveProdServ = "84111505", Cantidad = 1m,
                    ClaveUnidad = "ACT", Descripcion = "Pago de nómina",
                    ValorUnitario = 21_200.00m, Importe = 21_200.00m, ObjetoImp = "01"
                }
            ],
            ComplementoNomina = new ComplementoNominaPdf
            {
                Version           = "1.2",
                TipoNomina        = "O",
                FechaPago         = new DateTime(2024, 11, 15),
                FechaInicialPago  = new DateTime(2024, 11, 1),
                FechaFinalPago    = new DateTime(2024, 11, 15),
                NumDiasPagados    = 15m,
                TotalPercepciones = 21_200.00m,
                TotalDeducciones  = 3_180.00m,
                TotalOtrosPagos   = 380.68m,

                RegistroPatronal  = "Y4912345678",

                Curp                   = "MARG870924MJCLRN04",
                NumSeguridadSocial     = "37561482900",
                FechaInicioRelLaboral  = new DateTime(2018, 6, 1),
                Antiguedad             = "P331W",
                TipoContrato           = "01",
                TipoJornada            = "01",
                TipoRegimen            = "02",
                NumEmpleado            = "EMP-0047",
                Departamento           = "Ingeniería de Software",
                Puesto                 = "Desarrolladora Senior .NET",
                PeriodicidadPago       = "04",
                SalarioBaseCotApor     = 1_413.33m,
                SalarioDiarioIntegrado = 1_554.67m,
                ClaveEntFed            = "JAL",

                TotalSueldos = 15_000.00m,
                TotalGravado = 17_700.00m,
                TotalExento  = 3_500.00m,
                Percepciones =
                [
                    new PercepcionNominaPdf { TipoPercepcion = "001", Clave = "001", Concepto = "Sueldo quincenal", ImporteGravado = 15_000.00m, ImporteExento = 0m },
                    new PercepcionNominaPdf { TipoPercepcion = "019", Clave = "002", Concepto = "Prima vacacional", ImporteGravado = 0m, ImporteExento = 3_500.00m }
                ],
                TotalOtrasDeducciones  = 1_180.00m,
                TotalImpuestosRetenidos = 2_000.00m,
                Deducciones =
                [
                    new DeduccionNominaPdf { TipoDeduccion = "002", Clave = "001", Concepto = "ISR", Importe = 2_000.00m },
                    new DeduccionNominaPdf { TipoDeduccion = "001", Clave = "002", Concepto = "Cuotas IMSS", Importe = 1_180.00m }
                ],
                OtrosPagos =
                [
                    new OtroPagoNominaPdf { TipoOtroPago = "002", Clave = "001", Concepto = "Subsidio para el empleo aplicado", Importe = 380.68m }
                ]
            }
        };

        // Act
        var pdfBytes = PdfBuilder.Construir(cfdi);

        // Assert
        PdfTestHelpers.AssertValidPdf(pdfBytes);
    }

    [Fact]
    public void Construir_DesdeXmlRealNomina1_GeneraPdfValido()
    {
        // Arrange
        var xmlPath = Path.Combine(AppContext.BaseDirectory, "TestData", "nomina_sample1.xml");
        Assert.True(File.Exists(xmlPath), $"No se encontró el archivo de prueba: {xmlPath}");

        var xml = File.ReadAllText(xmlPath);
        var cfdi = CfdiXmlParser.FromXml(xml);

        Assert.Equal("N", cfdi.TipoComprobante);
        Assert.NotNull(cfdi.ComplementoNomina);
        Assert.NotEmpty(cfdi.ComplementoNomina.Percepciones);
        Assert.NotEmpty(cfdi.ComplementoNomina.Deducciones);

        // Act
        var pdfBytes = PdfBuilder.Construir(cfdi);

        // Assert
        PdfTestHelpers.AssertValidPdf(pdfBytes);
    }

    [Fact]
    public void Construir_DesdeXmlRealNomina2_GeneraPdfValido()
    {
        // Arrange
        var xmlPath = Path.Combine(AppContext.BaseDirectory, "TestData", "nomina_sample2.xml");
        Assert.True(File.Exists(xmlPath), $"No se encontró el archivo de prueba: {xmlPath}");

        var xml = File.ReadAllText(xmlPath);
        var cfdi = CfdiXmlParser.FromXml(xml);

        Assert.Equal("N", cfdi.TipoComprobante);
        Assert.NotNull(cfdi.ComplementoNomina);
        Assert.NotEmpty(cfdi.ComplementoNomina.Percepciones);

        // Act
        var pdfBytes = PdfBuilder.Construir(cfdi);

        // Assert
        PdfTestHelpers.AssertValidPdf(pdfBytes);
    }
}
