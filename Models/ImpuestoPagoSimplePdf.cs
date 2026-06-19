namespace NubeFiscal.PdfGenerator.Models;

/// <summary>Retención simplificada (impuesto e importe) del pago. Usada en <c>RetencionesP</c> dentro de <see cref="PagoPdf"/>.</summary>
public class ImpuestoPagoSimplePdf
{
    public string? Impuesto { get; set; }
    public decimal? Importe { get; set; }
}
