namespace NubeFiscal.PdfGenerator.Models;

public class ComplementoPagoPdf
{
    public string Version { get; set; } = "2.0";
    public TotalesPagoPdf? Totales { get; set; }
    public List<PagoPdf> Pagos { get; set; } = new();
}
