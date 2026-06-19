namespace NubeFiscal.PdfGenerator.Models;

/// <summary>Representa una línea de concepto del CFDI. Corresponde a la sección ⑤ Conceptos del diseño oficial del SAT.</summary>
public class ConceptoPdf
{
    public string? ClaveProdServ { get; set; }
    public string? NoIdentificacion { get; set; }
    public decimal Cantidad { get; set; }
    public string? ClaveUnidad { get; set; }
    public string? Unidad { get; set; }
    public string? Descripcion { get; set; }
    public decimal ValorUnitario { get; set; }
    public decimal Importe { get; set; }
    public decimal? Descuento { get; set; }
    public string? ObjetoImp { get; set; }
    public List<TrasladoPdf> Traslados { get; set; } = new();
    public List<RetencionPdf> Retenciones { get; set; } = new();
}
