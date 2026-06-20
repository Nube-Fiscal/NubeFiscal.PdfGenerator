# NubeFiscal.PdfGenerator

Genera representaciones impresas en PDF de **CFDIs 4.0 del SAT** de México, cumpliendo con todos los requisitos oficiales del Servicio de Administración Tributaria.

[![NuGet](https://img.shields.io/nuget/v/NubeFiscal.PdfGenerator)](https://www.nuget.org/packages/NubeFiscal.PdfGenerator)
[![NuGet Downloads](https://img.shields.io/nuget/dt/NubeFiscal.PdfGenerator)](https://www.nuget.org/packages/NubeFiscal.PdfGenerator)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![build](https://github.com/Nube-Fiscal/NubeFiscal.PdfGenerator/actions/workflows/build.yml/badge.svg)](https://github.com/Nube-Fiscal/NubeFiscal.PdfGenerator/actions/workflows/build.yml)
[![publish](https://github.com/Nube-Fiscal/NubeFiscal.PdfGenerator/actions/workflows/publish.yml/badge.svg)](https://github.com/Nube-Fiscal/NubeFiscal.PdfGenerator/actions/workflows/publish.yml)

---

## ¿Qué es una representación impresa de un CFDI?

El SAT establece que toda factura electrónica (CFDI) debe poder representarse en papel o formato digital legible. Esta representación impresa debe contener de forma visible y ordenada todos los datos del comprobante, el código QR de verificación y la leyenda **"Este documento es una representación impresa de un CFDI"**.

<img width="1009" height="746" alt="factura-ejemplo01" src="https://github.com/user-attachments/assets/b8eb6213-dd84-4ebb-9dd5-a4cfa0b2e33d" />

_Ejemplo de representación impresa (diseño oficial SAT)_

---

<details>
<summary><strong>Requisitos oficiales del SAT que cubre este paquete</strong></summary>

<br>

Según el [artículo 29-A del CFF](https://www.sat.gob.mx/minisitio/Factura/solicita_requisitos.htm), la representación impresa debe incluir:

### Datos del Emisor ①
- RFC del emisor
- Nombre o razón social
- Régimen fiscal según la Ley del ISR
- Código postal del domicilio fiscal

### Datos del Receptor ④
- RFC del receptor
- Nombre o razón social
- Régimen fiscal del receptor
- Código postal del domicilio fiscal del receptor
- Uso del CFDI

### Datos del Comprobante ③
- Folio fiscal (UUID) ②
- Código postal, fecha y hora de emisión
- Efecto del comprobante (Ingreso / Egreso / Traslado / Pago / Nómina)
- No. de serie del CSD del emisor ⑧②
- Exportación

### Conceptos ⑤
| Campo | Descripción |
|---|---|
| Clave del producto y/o servicio | Catálogo SAT |
| No. de identificación | SKU o clave interna |
| Cantidad | Con 6 decimales |
| Clave de unidad | Catálogo SAT (H87, E48, etc.) |
| Unidad | Descripción de la unidad |
| Valor unitario | Con 6 decimales |
| Importe | Con 2 decimales |
| Descuento | Cuando aplique |
| Objeto de impuesto | 01 / 02 / 03 / 04 |

### Impuestos por Concepto ⑥
Para cada concepto se detalla:
- Impuesto (ISR / IVA / IEPS)
- Tipo (Traslado / Retención)
- Base, Tipo Factor, Tasa o Cuota, Importe

### Totales ⑦
- Subtotal
- Descuento (si aplica)
- Impuestos Trasladados desglosados por tasa (IVA 16%, 8%, 0%, Exento)
- **Total**

### Sellos y certificación ⑧
- Sello digital del CFDI ②
- Sello digital del SAT
- Cadena original del complemento de certificación ⑧⑤
- No. de serie del certificado SAT ⑧②
- RFC del proveedor de certificación (PAC)
- Fecha y hora de certificación ⑧④
- **Código QR** de verificación en el portal del SAT ⑧①

</details>

---

## Lo que hace único a este paquete

Es el único NuGet con soporte completo para el diseño oficial del SAT CFDI 4.0:

| Tipo de comprobante | Soportado |
|---|---|
| Ingreso (I) | ✅ |
| Egreso (E) | ✅ |
| Traslado (T) | ✅ |
| Pago — Complemento de Pago 2.0 (P) | ✅ |
| Nómina — Complemento de Nómina 1.2 (N) | ✅ |
| Múltiples conceptos con impuestos por concepto | ✅ |
| Comprobantes cancelados con fecha de cancelación | ✅ |
| Exportación (No aplica / Definitiva / Temporal) | ✅ |
| Código QR de verificación SAT | ✅ |
| CFDI 3.3 (compatibilidad) | ✅ |

---

## Instalación

```bash
dotnet add package NubeFiscal.PdfGenerator
```

---

## Uso

### Desde XML crudo

La forma más sencilla: pasas el XML del CFDI y obtienes el PDF.

```csharp
using NubeFiscal.PdfGenerator.Services;

var xmlContent = await File.ReadAllTextAsync("mi-cfdi.xml");
var cfdi       = CfdiXmlParser.FromXml(xmlContent);
var pdfBytes   = PdfBuilder.Construir(cfdi);

await File.WriteAllBytesAsync("factura.pdf", pdfBytes);
```

### Desde base de datos

Si guardas los datos del CFDI en tu BD, construyes el modelo directamente sin necesidad del XML:

```csharp
using NubeFiscal.PdfGenerator.Models;
using NubeFiscal.PdfGenerator.Services;

var cfdi = new CfdiPdfData
{
    UUID                 = "704E83C3-C1CA-41C0-8225-E0AFE484969A",
    RFCEmisor            = "GOHE840111441",
    NombreEmisor         = "JOSE ELIAZAR GOMEZ HERERA",
    RegimenFiscalEmisor  = "621",
    LugarExpedicion      = "07530",
    RFCReceptor          = "WDM890106650",
    NombreReceptor       = "Walolar México S. DE R.L.DE C.V.",
    RegimenFiscalReceptor = "601",
    CodigoPostalReceptor  = "07530",
    UsoCFDI              = "I08",
    FechaEmision         = new DateTime(2019, 6, 27, 20, 7, 1),
    FechaTimbrado        = new DateTime(2019, 6, 27, 20, 11, 11),
    TipoComprobante      = "I",
    Moneda               = "MXN",
    MetodoPago           = "PPD",
    SubTotal             = 55000m,
    TotalImpuestosTrasladados = 8800m,
    Total                = 63800m,
    NoCertificado        = "00001000000413439058",
    NoCertificadoSAT     = "00001000000403258748",
    SelloCFDI            = "AiDHUEggSow8toaoY7t3a4vpcwkI3KxTDHOZrXC/4oaZPXpjin...",
    SelloSAT             = "SkjptLpfv6n1ePflhDfyMyxD6lSnveS6apJ+ZDJmNZrT0znQBepHg...",
    Conceptos =
    [
        new ConceptoPdf
        {
            ClaveProdServ = "56121900",
            Cantidad      = 1,
            ClaveUnidad   = "H87",
            Descripcion   = "Maniobras de Mobiliario",
            ValorUnitario = 55000m,
            Importe       = 55000m,
            ObjetoImp     = "02",
            Traslados     =
            [
                new TrasladoPdf { Impuesto = "002", TasaOCuota = "0.160000", Importe = 8800m }
            ]
        }
    ]
};

var pdfBytes = PdfBuilder.Construir(cfdi);
```

### Enriquecer desde XML cuando ya tienes datos de BD

Si tu registro en BD incluye el XML original pero ya tienes los campos básicos:

```csharp
cfdi.XmlCFDI = xmlGuardadoEnBd;
CfdiXmlParser.EnriquecerDesdeXml(cfdi); // sobrescribe solo lo que parsea del XML
var pdfBytes = PdfBuilder.Construir(cfdi);
```

<details>
<summary><strong>Complemento de Pago 2.0</strong></summary>

<br>

Cuando `TipoComprobante = "P"` y el XML incluye el complemento de pago, se parsea y renderiza automáticamente desde `CfdiXmlParser.FromXml()`. Para construirlo manualmente:

```csharp
cfdi.TipoComprobante = "P";
cfdi.ComplementoPago = new ComplementoPagoPdf
{
    Version = "2.0",
    Totales = new TotalesPagoPdf
    {
        TotalTrasladosBaseIVA16     = 55000m,
        TotalTrasladosImpuestoIVA16 = 8800m,
        MontoTotalPagos             = 63800m
    },
    Pagos =
    [
        new PagoPdf
        {
            FechaPago    = new DateTime(2019, 7, 15),
            FormaDePagoP = "03",  // Transferencia electrónica
            MonedaP      = "MXN",
            Monto        = 63800m,
            DoctoRelacionados =
            [
                new DoctoRelacionadoPdf
                {
                    IdDocumento      = "704E83C3-C1CA-41C0-8225-E0AFE484969A",
                    MonedaDR         = "MXN",
                    NumParcialidad   = 1,
                    ImpSaldoAnt      = 63800m,
                    ImpPagado        = 63800m,
                    ImpSaldoInsoluto = 0m,
                    ObjetoImpDR      = "02"
                }
            ]
        }
    ]
};
```

</details>

---

<details>
<summary><strong>Complemento de Nómina 1.2</strong></summary>

<br>

Cuando `TipoComprobante = "N"` y el XML incluye el complemento de nómina, se parsea y renderiza automáticamente desde `CfdiXmlParser.FromXml()`. Para construirlo manualmente:

```csharp
cfdi.TipoComprobante = "N";
cfdi.ComplementoNomina = new ComplementoNominaPdf
{
    Version           = "1.2",
    TipoNomina        = "O",                         // O = Ordinaria, E = Extraordinaria
    FechaPago         = new DateTime(2024, 6, 15),
    FechaInicialPago  = new DateTime(2024, 6, 1),
    FechaFinalPago    = new DateTime(2024, 6, 15),
    NumDiasPagados    = 15m,
    TotalPercepciones = 18000m,
    TotalDeducciones  = 3500m,
    TotalOtrosPagos   = 405.01m,

    // Datos del emisor (empresa)
    RegistroPatronal  = "C4512345678",

    // Datos del receptor (trabajador)
    Curp                   = "LOAJ840101HDFPRS09",
    NumSeguridadSocial     = "12345678900",
    FechaInicioRelLaboral  = new DateTime(2015, 3, 1),
    Antiguedad             = "P471W",                // formato ISO 8601 duration
    TipoContrato           = "01",                   // Contrato de trabajo por tiempo indeterminado
    TipoJornada            = "01",                   // Diurna
    TipoRegimen            = "02",                   // Sueldos y salarios
    NumEmpleado            = "EMP-001",
    Departamento           = "Tecnología",
    Puesto                 = "Desarrollador Senior",
    PeriodicidadPago       = "04",                   // Quincenal
    SalarioBaseCotApor     = 1200m,
    SalarioDiarioIntegrado = 1350m,
    ClaveEntFed            = "CMX",

    // Percepciones
    TotalSueldos = 18000m,
    TotalGravado = 12000m,
    TotalExento  = 6000m,
    Percepciones =
    [
        new PercepcionNominaPdf
        {
            TipoPercepcion = "001",   // Sueldos, Salarios  Rayas y Jornales
            Clave          = "001",
            Concepto       = "Sueldo quincenal",
            ImporteGravado = 12000m,
            ImporteExento  = 0m
        },
        new PercepcionNominaPdf
        {
            TipoPercepcion = "019",   // Horas extra
            Clave          = "002",
            Concepto       = "Prima vacacional",
            ImporteGravado = 0m,
            ImporteExento  = 6000m
        }
    ],

    // Deducciones
    TotalOtrasDeducciones  = 1500m,
    TotalImpuestosRetenidos = 2000m,
    Deducciones =
    [
        new DeduccionNominaPdf
        {
            TipoDeduccion = "002",    // ISR
            Clave         = "001",
            Concepto      = "ISR",
            Importe       = 2000m
        },
        new DeduccionNominaPdf
        {
            TipoDeduccion = "001",    // Seguridad social
            Clave         = "002",
            Concepto      = "IMSS",
            Importe       = 1500m
        }
    ],

    // Otros pagos (p.ej. subsidio al empleo)
    OtrosPagos =
    [
        new OtroPagoNominaPdf
        {
            TipoOtroPago = "002",     // Subsidio para el empleo aplicado
            Clave        = "001",
            Concepto     = "Subsidio al empleo",
            Importe      = 405.01m
        }
    ]
};

var pdfBytes = PdfBuilder.Construir(cfdi);
```

</details>

---

## Ejemplos

La carpeta `Samples/` contiene un proyecto de consola que genera 4 PDFs de demostración con datos ficticios, uno por cada tipo de comprobante soportado.

```bash
dotnet run --project Samples/NubeFiscal.PdfGenerator.Samples.csproj
```

Los archivos se generan en `Docs/` en la raíz del proyecto.

<details>
<summary><strong>Ingreso con múltiples conceptos</strong></summary>

<br>

<img width="686" height="926" alt="1-ingreso-multiconcepto" src="https://github.com/user-attachments/assets/22049866-38b8-4c8b-8e8b-32c513921ede" />

[Ver PDF de ejemplo](docs/1-ingreso-multiconcepto.pdf) — 9 conceptos: diésel, lubricantes, filtros, servicios, un concepto exento y uno con tasa 0%

</details>

<details>
<summary><strong>Complemento de Pago 2.0</strong></summary>

<br>

<img width="549" height="757" alt="2-complemento-pago" src="https://github.com/user-attachments/assets/ba15b536-2982-4f2c-9e37-251db508e0e3" />

[Ver PDF de ejemplo](docs/2-complemento-pago.pdf) — 2 pagos con documentos relacionados e impuestos del DR

</details>

<details>
<summary><strong>Retenciones (ISR + IVA)</strong></summary>

<br>

<img width="706" height="884" alt="3-retenciones" src="https://github.com/user-attachments/assets/528baf9c-7e02-450a-a500-83d260273d68" />

[Ver PDF de ejemplo](docs/3-retenciones.pdf) — Honorarios profesionales con ISR e IVA retenido por concepto

</details>

<details>
<summary><strong>Nómina — Complemento de Nómina 1.2</strong></summary>

<br>

<img width="622" height="854" alt="4-nomina" src="https://github.com/user-attachments/assets/0da8eca3-07ca-4390-b588-c0818a7575c2" />

[Ver PDF de ejemplo](docs/4-nomina.pdf) — Nómina quincenal con 4 percepciones, 3 deducciones y subsidio al empleo

</details>

---

## Catálogos incluidos

El paquete resuelve automáticamente las claves del SAT a su descripción completa:

- **Uso CFDI:** G01, G02, G03, I01–I08, D01–D10, P01, S01, CP01
- **Régimen fiscal:** 601–627 (todos los regímenes vigentes)
- **Forma de pago:** 01–99
- **Método de pago:** PUE, PPD
- **Tipo de comprobante:** I, E, T, P, N
- **Impuestos:** 001 ISR, 002 IVA, 003 IEPS
- **Objeto de impuesto:** 01–04
- **Monedas:** MXN, USD, EUR, CAD, GBP, JPY, CNY y más
- **Exportación:** No aplica / Definitiva / Temporal

---

## Dependencias

| Paquete | Versión | Uso |
|---|---|---|
| [QuestPDF](https://www.questpdf.com/) | 2024.10.4 | Renderizado PDF (licencia Community) |
| [QRCoder](https://github.com/codebude/QRCoder) | 1.6.0 | Código QR de verificación SAT |

---

## ¿Por qué existe este paquete?

Trabajando en un proyecto propio necesitaba generar las representaciones gráficas de CFDIs a partir de sus XMLs. El problema: **ninguna librería gratuita las generaba con el diseño oficial del SAT**. La gran mayoría de las soluciones disponibles te obligan a pagar un servicio externo y esperar que te liberen un API Key para poder usarlo.

Decidí construirlo desde cero, con el diseño correcto, y liberarlo como open source para que cualquier desarrollador en México pueda usarlo sin depender de terceros ni pagar por algo que debería ser libre.

---

## Contribuciones

¡Las PRs son bienvenidas! Si tienes mejoras al diseño, soporte para nuevos complementos, correcciones o cualquier idea, adelante:

1. Haz fork del repositorio
2. Crea tu rama: `git checkout -b mi-mejora`
3. Haz commit de tus cambios: `git commit -m "Descripción de la mejora"`
4. Abre un Pull Request

---

## Licencia

MIT — open source bajo [Nube Fiscal](https://github.com/Nube-Fiscal).
