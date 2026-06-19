namespace NubeFiscal.PdfGenerator.Models;

/// <summary>Impuesto retenido (ISR / IVA) de un concepto. Aparece en la columna de impuestos de la sección ⑤ Conceptos y se refleja en los totales ⑦.</summary>
public class RetencionPdf
{
    public string? Impuesto { get; set; }
    public decimal? Base { get; set; }
    public decimal? Importe { get; set; }
}
