namespace NubeFiscal.PdfGenerator.Models;

/// <summary>Representa un pago individual dentro del Complemento de Pago 2.0. Un comprobante puede tener múltiples pagos.</summary>
public class PagoPdf
{
    public DateTime? FechaPago { get; set; }
    public string? FormaDePagoP { get; set; }
    public string? MonedaP { get; set; }
    public decimal? TipoCambioP { get; set; }
    public decimal Monto { get; set; }
    public string? NumOperacion { get; set; }
    public List<DoctoRelacionadoPdf> DoctoRelacionados { get; set; } = new();
    public List<ImpuestoPagoSimplePdf> RetencionesP { get; set; } = new();
    public List<ImpuestoPagoDetalladoPdf> TrasladosP { get; set; } = new();
}
