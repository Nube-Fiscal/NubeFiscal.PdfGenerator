namespace NubeFiscal.PdfGenerator.Models;

/// <summary>Modelo principal del CFDI. Contiene todos los campos necesarios para generar la representación impresa en PDF.</summary>
public class CfdiPdfData
{
    public long Id { get; set; }
    public int IdRazonSocial { get; set; }
    public string RFCRazonSocial { get; set; } = string.Empty;
    public string UUID { get; set; } = string.Empty;
    public string? RFCEmisor { get; set; }
    public string? RFCReceptor { get; set; }
    public string? Serie { get; set; }
    public string? Folio { get; set; }
    public DateTime FechaEmision { get; set; }
    public DateTime? FechaTimbrado { get; set; }
    public decimal? Total { get; set; }
    public string? Moneda { get; set; }
    public string? TipoComprobante { get; set; }
    public string EstatusSAT { get; set; } = string.Empty;
    public DateTime? FechaCancelacion { get; set; }
    public string AnioMes { get; set; } = string.Empty;
    public string TipoDescarga { get; set; } = string.Empty;
    public string? XmlCFDI { get; set; }
    public string? PackageId { get; set; }
    public string? NombrePaqueteZIP { get; set; }
    public DateTime FechaDescarga { get; set; }
    public string? RutaPdf { get; set; }

    public string? NombreEmisor { get; set; }
    public string? RegimenFiscalEmisor { get; set; }
    public string? NombreReceptor { get; set; }
    public string? RegimenFiscalReceptor { get; set; }
    public string? UsoCFDI { get; set; }
    public string? VersionCfdi { get; set; }
    public string? Exportacion { get; set; }
    public string? CodigoPostalEmisor { get; set; }
    public string? CodigoPostalReceptor { get; set; }
    public string? LugarExpedicion { get; set; }
    public decimal? SubTotal { get; set; }
    public decimal? Descuento { get; set; }
    public decimal? TotalImpuestosTrasladados { get; set; }
    public decimal? TotalImpuestosRetenidos { get; set; }
    public string? SelloSAT { get; set; }
    public string? SelloCFDI { get; set; }
    public string? CadenaOriginal { get; set; }
    public string? RfcProveedorCertificacion { get; set; }
    public string? NoCertificado { get; set; }
    public string? NoCertificadoSAT { get; set; }
    public string? FormaPago { get; set; }
    public string? MetodoPago { get; set; }
    public List<ConceptoPdf> Conceptos { get; set; } = new();

    /// <summary>Poblado únicamente cuando TipoComprobante == "P".</summary>
    public ComplementoPagoPdf? ComplementoPago { get; set; }

    /// <summary>Poblado únicamente cuando TipoComprobante == "N".</summary>
    public ComplementoNominaPdf? ComplementoNomina { get; set; }
}
