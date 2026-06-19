namespace NubeFiscal.PdfGenerator.Models;

public class ImpuestoPagoDetalladoPdf
{
    public string? Impuesto { get; set; }
    public string? TipoFactor { get; set; }
    public decimal? Base { get; set; }
    public decimal? TasaOCuota { get; set; }
    public decimal? Importe { get; set; }
}
