namespace NubeFiscal.PdfGenerator.Models;

/// <summary>Raíz del Complemento de Pago 2.0. Poblado únicamente cuando <c>TipoComprobante == "P"</c>. Se renderiza después de la tabla de conceptos.</summary>
public class ComplementoPagoPdf
{
    public string Version { get; set; } = "2.0";
    public TotalesPagoPdf? Totales { get; set; }
    public List<PagoPdf> Pagos { get; set; } = new();
}
