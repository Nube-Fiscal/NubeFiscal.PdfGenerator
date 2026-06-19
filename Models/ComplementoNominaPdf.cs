namespace NubeFiscal.PdfGenerator.Models;

public class ComplementoNominaPdf
{
    public string Version { get; set; } = "1.2";
    public string? TipoNomina { get; set; }
    public DateTime? FechaPago { get; set; }
    public DateTime? FechaInicialPago { get; set; }
    public DateTime? FechaFinalPago { get; set; }
    public decimal? NumDiasPagados { get; set; }
    public decimal? TotalPercepciones { get; set; }
    public decimal? TotalDeducciones { get; set; }
    public decimal? TotalOtrosPagos { get; set; }

    public string? RegistroPatronal { get; set; }

    public string? Curp { get; set; }
    public string? NumSeguridadSocial { get; set; }
    public DateTime? FechaInicioRelLaboral { get; set; }
    public string? Antiguedad { get; set; }
    public string? TipoContrato { get; set; }
    public string? TipoJornada { get; set; }
    public string? TipoRegimen { get; set; }
    public string? NumEmpleado { get; set; }
    public string? Departamento { get; set; }
    public string? Puesto { get; set; }
    public string? PeriodicidadPago { get; set; }
    public decimal? SalarioBaseCotApor { get; set; }
    public decimal? SalarioDiarioIntegrado { get; set; }
    public string? ClaveEntFed { get; set; }

    public decimal? TotalSueldos { get; set; }
    public decimal? TotalGravado { get; set; }
    public decimal? TotalExento { get; set; }
    public List<PercepcionNominaPdf> Percepciones { get; set; } = new();

    public decimal? TotalOtrasDeducciones { get; set; }
    public decimal? TotalImpuestosRetenidos { get; set; }
    public List<DeduccionNominaPdf> Deducciones { get; set; } = new();

    public List<OtroPagoNominaPdf> OtrosPagos { get; set; } = new();
}
