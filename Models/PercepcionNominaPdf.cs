namespace NubeFiscal.PdfGenerator.Models;

/// <summary>Concepto de percepción (sueldo, prima, bono, etc.) del Complemento de Nómina. Se muestra en la tabla de Percepciones con importes gravado y exento.</summary>
public class PercepcionNominaPdf
{
    public string? TipoPercepcion { get; set; }
    public string? Clave { get; set; }
    public string? Concepto { get; set; }
    public decimal? ImporteGravado { get; set; }
    public decimal? ImporteExento { get; set; }
}
