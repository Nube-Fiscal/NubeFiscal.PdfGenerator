namespace NubeFiscal.PdfGenerator.Models;

public class OtroPagoNominaPdf
{
    public string? TipoOtroPago { get; set; }
    public string? Clave { get; set; }
    public string? Concepto { get; set; }
    public decimal? Importe { get; set; }
}
