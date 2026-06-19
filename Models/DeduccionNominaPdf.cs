namespace NubeFiscal.PdfGenerator.Models;

public class DeduccionNominaPdf
{
    public string? TipoDeduccion { get; set; }
    public string? Clave { get; set; }
    public string? Concepto { get; set; }
    public decimal? Importe { get; set; }
}
