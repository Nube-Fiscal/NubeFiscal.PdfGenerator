namespace NubeFiscal.PdfGenerator.Models;

public class PercepcionNominaPdf
{
    public string? TipoPercepcion { get; set; }
    public string? Clave { get; set; }
    public string? Concepto { get; set; }
    public decimal? ImporteGravado { get; set; }
    public decimal? ImporteExento { get; set; }
}
