namespace NubeFiscal.PdfGenerator.Models;

/// <summary>Concepto de deducción (ISR, IMSS, préstamos, etc.) del Complemento de Nómina. Se muestra en la tabla de Deducciones.</summary>
public class DeduccionNominaPdf
{
    public string? TipoDeduccion { get; set; }
    public string? Clave { get; set; }
    public string? Concepto { get; set; }
    public decimal? Importe { get; set; }
}
