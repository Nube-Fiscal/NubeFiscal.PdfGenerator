namespace NubeFiscal.PdfGenerator.Models;

/// <summary>Otro pago aplicado al empleado (p. ej. subsidio al empleo). Se muestra en la tabla de Otros Pagos y suma al neto a pagar.</summary>
public class OtroPagoNominaPdf
{
    public string? TipoOtroPago { get; set; }
    public string? Clave { get; set; }
    public string? Concepto { get; set; }
    public decimal? Importe { get; set; }
}
