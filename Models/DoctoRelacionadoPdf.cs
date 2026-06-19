namespace NubeFiscal.PdfGenerator.Models;

/// <summary>Factura o comprobante que se liquida con un pago. Cada <see cref="PagoPdf"/> puede relacionar uno o más documentos.</summary>
public class DoctoRelacionadoPdf
{
    public string? IdDocumento { get; set; }
    public string? Serie { get; set; }
    public string? Folio { get; set; }
    public string? MonedaDR { get; set; }
    public decimal? EquivalenciaDR { get; set; }
    public int? NumParcialidad { get; set; }
    public decimal? ImpSaldoAnt { get; set; }
    public decimal? ImpPagado { get; set; }
    public decimal? ImpSaldoInsoluto { get; set; }
    public string? ObjetoImpDR { get; set; }
    public List<ImpuestoPagoDetalladoPdf> RetencionesDR { get; set; } = new();
    public List<ImpuestoPagoDetalladoPdf> TrasladosDR { get; set; } = new();
}
