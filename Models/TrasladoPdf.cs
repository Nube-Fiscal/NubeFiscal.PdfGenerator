namespace NubeFiscal.PdfGenerator.Models;

/// <summary>Impuesto trasladado (IVA / IEPS) de un concepto. Aparece en la columna de impuestos de la sección ⑤ Conceptos.</summary>
public class TrasladoPdf
{
    public string? Impuesto { get; set; }
    public string? TasaOCuota { get; set; }
    public decimal? Importe { get; set; }
}
