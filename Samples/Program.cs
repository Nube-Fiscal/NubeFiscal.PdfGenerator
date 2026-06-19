using NubeFiscal.PdfGenerator.Models;
using NubeFiscal.PdfGenerator.Services;

const string SelloCFDI =
    "Kj8mXpQ2vNwL5rT9aHcYuE3oBsFdGiZnR7qW1lAeUyV4kPtJ6hMxC0sD+bNvL2mKpR8wQjT" +
    "uE5aHcYoB3sFdGiZnR7qW1lAeUyV4kPtJ6hMxC0sDbNvL2mKpR8wQjTuE5aHcYoBsFdGiZn" +
    "R7qW1lAeUyV4kPtJ6hMxC0sDbNvL2mKpR8wQjTuE5aHcYoBsFdGiZnR7qW1lAeUyV4kPtJ6" +
    "hMxC0sDbNvL2mKpR8wQjTuE5aHcYoBsFdGiZnR7qW1lAeUyV4kPtJ6hMxC0sDbNvL2mKpR8=";

const string SelloSAT =
    "Xv9nBqLmP3kT7wRoJ5cEuZaGfHsYiD2eV8yU1tA4rNxW6hFbM0pSjQdKgOlCzI+nBqLmP3kT" +
    "7wRoJ5cEuZaGfHsYiD2eV8yU1tA4rNxW6hFbM0pSjQdKgOlCzInBqLmP3kT7wRoJ5cEuZaGf" +
    "HsYiD2eV8yU1tA4rNxW6hFbM0pSjQdKgOlCzInBqLmP3kT7wRoJ5cEuZaGfHsYiD2eV8yU1t" +
    "A4rNxW6hFbM0pSjQdKgOlCzInBqLmP3kT7wRoJ5cEuZaGfHsYiD2eV8yU1tA4rNxW6hFbM0=";

const string NoCertificado    = "00001000000504465028";
const string NoCertificadoSAT = "00001000000403258748";
const string RfcPAC           = "SAT970701NN3";

// Desde samples/bin/Debug/net10.0/ subimos 4 niveles hasta la raíz del proyecto
var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../.."));
var outDir = Path.Combine(projectRoot, "docs");
Directory.CreateDirectory(outDir);

GenerarIngresoMultiConcepto(outDir);
GenerarComplementoPago(outDir);
GenerarRetenciones(outDir);
GenerarNomina(outDir);

Console.WriteLine($"PDFs generados en: {outDir}");

// ── 1. Ingreso con múltiples conceptos ───────────────────────────────────────

static void GenerarIngresoMultiConcepto(string dir)
{
    var uuid = "A3F7C2B1-8D45-4E90-BF16-0C92E3A57D84";
    var cfdi = new CfdiPdfData
    {
        UUID                      = uuid,
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
        NoCertificado             = NoCertificado,
        NoCertificadoSAT          = NoCertificadoSAT,
        SelloCFDI                 = SelloCFDI,
        SelloSAT                  = SelloSAT,
        RfcProveedorCertificacion = RfcPAC,
        FechaCancelacion          = null,
        EstatusSAT                = "Vigente",
        CadenaOriginal            = $"||1.1|{uuid}|2024-11-08T14:33:18|{RfcPAC}|{SelloCFDI}|{NoCertificadoSAT}||",
        Conceptos =
        [
            new ConceptoPdf
            {
                ClaveProdServ = "15101514", NoIdentificacion = "DIES-001",
                Cantidad = 5000.000000m, ClaveUnidad = "LTR", Unidad = "Litro",
                Descripcion = "Diesel automotriz (B5)",
                ValorUnitario = 22.480000m, Importe = 112_400.00m,
                ObjetoImp = "02",
                Traslados = [new TrasladoPdf { Impuesto = "002", TasaOCuota = "0.160000", Importe = 17_984.00m }]
            },
            new ConceptoPdf
            {
                ClaveProdServ = "15101514", NoIdentificacion = "DIES-002",
                Cantidad = 1200.000000m, ClaveUnidad = "LTR", Unidad = "Litro",
                Descripcion = "Diesel premium (B20)",
                ValorUnitario = 24.150000m, Importe = 28_980.00m,
                ObjetoImp = "02",
                Traslados = [new TrasladoPdf { Impuesto = "002", TasaOCuota = "0.160000", Importe = 4_636.80m }]
            },
            new ConceptoPdf
            {
                ClaveProdServ = "15121806", NoIdentificacion = "LUB-010",
                Cantidad = 80.000000m, ClaveUnidad = "LTR", Unidad = "Litro",
                Descripcion = "Aceite de motor 15W-40 para motores diésel",
                ValorUnitario = 95.000000m, Importe = 7_600.00m,
                Descuento = 760.00m,
                ObjetoImp = "02",
                Traslados = [new TrasladoPdf { Impuesto = "002", TasaOCuota = "0.160000", Importe = 1_094.40m }]
            },
            new ConceptoPdf
            {
                ClaveProdServ = "25172200", NoIdentificacion = "FIL-032",
                Cantidad = 24.000000m, ClaveUnidad = "H87", Unidad = "Pieza",
                Descripcion = "Filtro de aceite para motor Cummins ISX15",
                ValorUnitario = 285.000000m, Importe = 6_840.00m,
                Descuento = 684.00m,
                ObjetoImp = "02",
                Traslados = [new TrasladoPdf { Impuesto = "002", TasaOCuota = "0.160000", Importe = 985.44m }]
            },
            new ConceptoPdf
            {
                ClaveProdServ = "25172200", NoIdentificacion = "FIL-033",
                Cantidad = 24.000000m, ClaveUnidad = "H87", Unidad = "Pieza",
                Descripcion = "Filtro de combustible separador agua/diésel",
                ValorUnitario = 320.000000m, Importe = 7_680.00m,
                Descuento = 768.00m,
                ObjetoImp = "02",
                Traslados = [new TrasladoPdf { Impuesto = "002", TasaOCuota = "0.160000", Importe = 1_105.92m }]
            },
            new ConceptoPdf
            {
                ClaveProdServ = "78101803", NoIdentificacion = "SRV-100",
                Cantidad = 8.000000m, ClaveUnidad = "E48", Unidad = "Servicio",
                Descripcion = "Servicio de mantenimiento preventivo (cambio de aceite, filtros y revisión general)",
                ValorUnitario = 1_193.750000m, Importe = 9_550.00m,
                Descuento = 1_537.00m,
                ObjetoImp = "02",
                Traslados = [new TrasladoPdf { Impuesto = "002", TasaOCuota = "0.160000", Importe = 1_281.12m }]
            },
            new ConceptoPdf
            {
                ClaveProdServ = "78101803", NoIdentificacion = "SRV-101",
                Cantidad = 2.000000m, ClaveUnidad = "E48", Unidad = "Servicio",
                Descripcion = "Diagnóstico electrónico de motor (escáner OBD2)",
                ValorUnitario = 700.000000m, Importe = 1_400.00m,
                ObjetoImp = "02",
                Traslados = [new TrasladoPdf { Impuesto = "002", TasaOCuota = "0.160000", Importe = 224.00m }]
            },
            new ConceptoPdf
            {
                ClaveProdServ = "72154301", NoIdentificacion = "ENV-009",
                Cantidad = 50.000000m, ClaveUnidad = "H87", Unidad = "Pieza",
                Descripcion = "Tambor metálico 200L para almacenamiento de lubricantes",
                ValorUnitario = 200.000000m, Importe = 10_000.00m,
                ObjetoImp = "01",
                Traslados = []
            },
            new ConceptoPdf
            {
                ClaveProdServ = "78101803", NoIdentificacion = "SRV-200",
                Cantidad = 1.000000m, ClaveUnidad = "E48", Unidad = "Servicio",
                Descripcion = "Flete y maniobras de entrega en almacén cliente",
                ValorUnitario = 3_000.000000m, Importe = 3_000.00m,
                ObjetoImp = "02",
                Traslados = [new TrasladoPdf { Impuesto = "002", TasaOCuota = "0.000000", Importe = 0.00m }]
            },
        ]
    };

    File.WriteAllBytes(Path.Combine(dir, "1-ingreso-multiconcepto.pdf"), PdfBuilder.Construir(cfdi));
    Console.WriteLine("✓ 1-ingreso-multiconcepto.pdf");
}

// ── 2. Complemento de Pago 2.0 ───────────────────────────────────────────────

static void GenerarComplementoPago(string dir)
{
    var uuid = "B5E9D3C2-1F74-4A08-9E23-7B84C0F16A53";
    var cfdi = new CfdiPdfData
    {
        UUID                      = uuid,
        RFCEmisor                 = "DCN930415TF8",
        NombreEmisor              = "DISTRIBUIDORA CENTRAL DEL NORTE S.A. DE C.V.",
        RegimenFiscalEmisor       = "601",
        LugarExpedicion           = "64000",
        RFCReceptor               = "TLH850603M23",
        NombreReceptor            = "TRANSPORTES Y LOGÍSTICA HERNÁNDEZ HNOS. S.A. DE C.V.",
        RegimenFiscalReceptor     = "601",
        CodigoPostalReceptor      = "44100",
        UsoCFDI                   = "CP01",
        FechaEmision              = new DateTime(2024, 12, 3, 9, 17, 44),
        FechaTimbrado             = new DateTime(2024, 12, 3, 9, 18, 02),
        TipoComprobante           = "P",
        Moneda                    = "XXX",
        Exportacion               = "01",
        SubTotal                  = 0m,
        Total                     = 0m,
        NoCertificado             = NoCertificado,
        NoCertificadoSAT          = NoCertificadoSAT,
        SelloCFDI                 = SelloCFDI,
        SelloSAT                  = SelloSAT,
        RfcProveedorCertificacion = RfcPAC,
        EstatusSAT                = "Vigente",
        CadenaOriginal            = $"||1.1|{uuid}|2024-12-03T09:18:02|{RfcPAC}|{SelloCFDI}|{NoCertificadoSAT}||",
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
                    Monto        = 212_773.32m,
                    NumOperacion = "0098231174",
                    DoctoRelacionados =
                    [
                        new DoctoRelacionadoPdf
                        {
                            IdDocumento      = "A3F7C2B1-8D45-4E90-BF16-0C92E3A57D84",
                            Serie            = "A",
                            Folio            = "1042",
                            MonedaDR         = "MXN",
                            NumParcialidad   = 1,
                            ImpSaldoAnt      = 212_773.32m,
                            ImpPagado        = 212_773.32m,
                            ImpSaldoInsoluto = 0m,
                            ObjetoImpDR      = "02",
                            TrasladosDR =
                            [
                                new ImpuestoPagoDetalladoPdf
                                {
                                    Impuesto   = "002",
                                    TipoFactor = "Tasa",
                                    Base       = 183_701.00m,
                                    TasaOCuota = 0.160000m,
                                    Importe    = 29_392.16m
                                }
                            ]
                        }
                    ],
                    TrasladosP =
                    [
                        new ImpuestoPagoDetalladoPdf
                        {
                            Impuesto   = "002",
                            TipoFactor = "Tasa",
                            Base       = 183_701.00m,
                            TasaOCuota = 0.160000m,
                            Importe    = 29_392.16m
                        }
                    ]
                },
                new PagoPdf
                {
                    FechaPago    = new DateTime(2024, 12, 2, 15, 45, 0),
                    FormaDePagoP = "03",
                    MonedaP      = "MXN",
                    Monto        = 181_626.68m,
                    NumOperacion = "0098415803",
                    DoctoRelacionados =
                    [
                        new DoctoRelacionadoPdf
                        {
                            IdDocumento      = "C9A4F1E0-3B62-4D87-AF05-6E71B2D38C90",
                            Serie            = "A",
                            Folio            = "1039",
                            MonedaDR         = "MXN",
                            NumParcialidad   = 2,
                            ImpSaldoAnt      = 363_253.36m,
                            ImpPagado        = 181_626.68m,
                            ImpSaldoInsoluto = 181_626.68m,
                            ObjetoImpDR      = "02",
                            TrasladosDR =
                            [
                                new ImpuestoPagoDetalladoPdf
                                {
                                    Impuesto   = "002",
                                    TipoFactor = "Tasa",
                                    Base       = 156_575.59m,
                                    TasaOCuota = 0.160000m,
                                    Importe    = 25_052.09m
                                }
                            ]
                        }
                    ],
                    TrasladosP =
                    [
                        new ImpuestoPagoDetalladoPdf
                        {
                            Impuesto   = "002",
                            TipoFactor = "Tasa",
                            Base       = 156_575.59m,
                            TasaOCuota = 0.160000m,
                            Importe    = 25_052.09m
                        }
                    ]
                }
            ]
        }
    };

    File.WriteAllBytes(Path.Combine(dir, "2-complemento-pago.pdf"), PdfBuilder.Construir(cfdi));
    Console.WriteLine("✓ 2-complemento-pago.pdf");
}

// ── 3. Ingreso con retenciones (ISR + IVA) ───────────────────────────────────

static void GenerarRetenciones(string dir)
{
    var uuid = "D2C8A5F4-6E31-4B79-8D14-3A95E07F2B61";
    var cfdi = new CfdiPdfData
    {
        UUID                     = uuid,
        RFCEmisor                = "CEL760823JK5",
        NombreEmisor             = "CONSULTORES ESTRATÉGICOS LUNA S.C.",
        RegimenFiscalEmisor      = "612",
        LugarExpedicion          = "06600",
        RFCReceptor              = "GIA890301QP7",
        NombreReceptor           = "GRUPO INDUSTRIAL ALTAMIRA S.A. DE C.V.",
        RegimenFiscalReceptor    = "601",
        CodigoPostalReceptor     = "89600",
        UsoCFDI                  = "G03",
        FechaEmision             = new DateTime(2024, 10, 15, 10, 5, 30),
        FechaTimbrado            = new DateTime(2024, 10, 15, 10, 6, 12),
        TipoComprobante          = "I",
        Moneda                   = "MXN",
        FormaPago                = "03",
        MetodoPago               = "PUE",
        Exportacion              = "01",
        Serie                    = "HC",
        Folio                    = "318",
        SubTotal                 = 95_000.00m,
        TotalImpuestosTrasladados = 15_200.00m,
        TotalImpuestosRetenidos  = 11_875.00m,
        Total                    = 98_325.00m,
        NoCertificado            = NoCertificado,
        NoCertificadoSAT         = NoCertificadoSAT,
        SelloCFDI                = SelloCFDI,
        SelloSAT                 = SelloSAT,
        RfcProveedorCertificacion = RfcPAC,
        EstatusSAT               = "Vigente",
        CadenaOriginal           = $"||1.1|{uuid}|2024-10-15T10:06:12|{RfcPAC}|{SelloCFDI}|{NoCertificadoSAT}||",
        Conceptos =
        [
            new ConceptoPdf
            {
                ClaveProdServ = "80141600", NoIdentificacion = "CONS-01",
                Cantidad = 1.000000m, ClaveUnidad = "E48", Unidad = "Servicio",
                Descripcion = "Consultoría en procesos industriales y optimización de cadena de suministro — Fase 1 (diagnóstico y análisis)",
                ValorUnitario = 50_000.000000m, Importe = 50_000.00m,
                ObjetoImp = "02",
                Traslados =
                [
                    new TrasladoPdf { Impuesto = "002", TasaOCuota = "0.160000", Importe = 8_000.00m }
                ],
                Retenciones =
                [
                    new RetencionPdf { Impuesto = "001", Base = 50_000.00m, Importe = 5_000.00m },
                    new RetencionPdf { Impuesto = "002", Base = 50_000.00m, Importe = 5_333.33m }
                ]
            },
            new ConceptoPdf
            {
                ClaveProdServ = "80141600", NoIdentificacion = "CONS-02",
                Cantidad = 1.000000m, ClaveUnidad = "E48", Unidad = "Servicio",
                Descripcion = "Consultoría en procesos industriales — Fase 2 (implementación y capacitación al equipo operativo)",
                ValorUnitario = 45_000.000000m, Importe = 45_000.00m,
                ObjetoImp = "02",
                Traslados =
                [
                    new TrasladoPdf { Impuesto = "002", TasaOCuota = "0.160000", Importe = 7_200.00m }
                ],
                Retenciones =
                [
                    new RetencionPdf { Impuesto = "001", Base = 45_000.00m, Importe = 4_500.00m },
                    new RetencionPdf { Impuesto = "002", Base = 45_000.00m, Importe = 2_041.67m }
                ]
            }
        ]
    };

    File.WriteAllBytes(Path.Combine(dir, "3-retenciones.pdf"), PdfBuilder.Construir(cfdi));
    Console.WriteLine("✓ 3-retenciones.pdf");
}

// ── 4. Nómina 1.2 ────────────────────────────────────────────────────────────

static void GenerarNomina(string dir)
{
    var uuid = "E7B0D4A3-9C52-4F86-2E47-5D18F3C09A72";
    var cfdi = new CfdiPdfData
    {
        UUID                     = uuid,
        RFCEmisor                = "TEC840601NX3",
        NombreEmisor             = "SOLUCIONES TECNOLÓGICAS NEXO S.A. DE C.V.",
        RegimenFiscalEmisor      = "601",
        LugarExpedicion          = "45010",
        RFCReceptor              = "MARG870924QT5",
        NombreReceptor           = "GARCÍA RAMOS MARIANA",
        RegimenFiscalReceptor    = "605",
        CodigoPostalReceptor     = "45010",
        UsoCFDI                  = "CN01",
        FechaEmision             = new DateTime(2024, 11, 15, 23, 59, 59),
        FechaTimbrado            = new DateTime(2024, 11, 16, 0, 0, 24),
        TipoComprobante          = "N",
        Moneda                   = "MXN",
        FormaPago                = "03",
        MetodoPago               = "PUE",
        Exportacion              = "01",
        SubTotal                 = 21_200.00m,
        Total                    = 21_200.00m,
        NoCertificado            = NoCertificado,
        NoCertificadoSAT         = NoCertificadoSAT,
        SelloCFDI                = SelloCFDI,
        SelloSAT                 = SelloSAT,
        RfcProveedorCertificacion = RfcPAC,
        EstatusSAT               = "Vigente",
        CadenaOriginal           = $"||1.1|{uuid}|2024-11-16T00:00:24|{RfcPAC}|{SelloCFDI}|{NoCertificadoSAT}||",
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
                new PercepcionNominaPdf
                {
                    TipoPercepcion = "001",
                    Clave          = "001",
                    Concepto       = "Sueldo quincenal",
                    ImporteGravado = 15_000.00m,
                    ImporteExento  = 0m
                },
                new PercepcionNominaPdf
                {
                    TipoPercepcion = "019",
                    Clave          = "002",
                    Concepto       = "Prima vacacional",
                    ImporteGravado = 0m,
                    ImporteExento  = 3_500.00m
                },
                new PercepcionNominaPdf
                {
                    TipoPercepcion = "045",
                    Clave          = "003",
                    Concepto       = "Vales de despensa",
                    ImporteGravado = 1_700.00m,
                    ImporteExento  = 0m
                },
                new PercepcionNominaPdf
                {
                    TipoPercepcion = "010",
                    Clave          = "004",
                    Concepto       = "Premio de puntualidad",
                    ImporteGravado = 1_000.00m,
                    ImporteExento  = 0m
                }
            ],

            TotalOtrasDeducciones  = 1_180.00m,
            TotalImpuestosRetenidos = 2_000.00m,
            Deducciones =
            [
                new DeduccionNominaPdf
                {
                    TipoDeduccion = "002",
                    Clave         = "001",
                    Concepto      = "ISR",
                    Importe       = 2_000.00m
                },
                new DeduccionNominaPdf
                {
                    TipoDeduccion = "001",
                    Clave         = "002",
                    Concepto      = "Cuotas IMSS",
                    Importe       = 890.00m
                },
                new DeduccionNominaPdf
                {
                    TipoDeduccion = "006",
                    Clave         = "003",
                    Concepto       = "Descuento por préstamo personal (caja de ahorro)",
                    Importe       = 290.00m
                }
            ],

            OtrosPagos =
            [
                new OtroPagoNominaPdf
                {
                    TipoOtroPago = "002",
                    Clave        = "001",
                    Concepto     = "Subsidio para el empleo aplicado",
                    Importe      = 380.68m
                }
            ]
        }
    };

    File.WriteAllBytes(Path.Combine(dir, "4-nomina.pdf"), PdfBuilder.Construir(cfdi));
    Console.WriteLine("✓ 4-nomina.pdf");
}
