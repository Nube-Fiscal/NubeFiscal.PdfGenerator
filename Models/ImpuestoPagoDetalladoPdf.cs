namespace NubeFiscal.PdfGenerator.Models;

/// <summary>Impuesto desglosado (base, tasa y importe) usado en traslados y retenciones de documentos relacionados (<c>TrasladosDR</c>, <c>RetencionesDR</c>) y del pago (<c>TrasladosP</c>).</summary>
public class ImpuestoPagoDetalladoPdf
{
    public string? Impuesto { get; set; }
    public string? TipoFactor { get; set; }
    public decimal? Base { get; set; }
    public decimal? TasaOCuota { get; set; }
    public decimal? Importe { get; set; }
}
