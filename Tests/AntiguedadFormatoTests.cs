using NubeFiscal.PdfGenerator.Services;

namespace NubeFiscal.PdfGenerator.Tests;

public class AntiguedadFormatoTests
{
    [Theory]
    [InlineData("P331W", "6 años, 19 semanas")]
    [InlineData("P52W", "1 año")]
    [InlineData("P104W", "2 años")]
    [InlineData("P20W", "20 semanas")]
    [InlineData("P1W", "1 semana")]
    [InlineData("P0W", "0 semanas")]
    [InlineData("P24D", "24 días")]
    [InlineData("P1D", "1 día")]
    [InlineData("P0D", "0 días")]
    [InlineData("P1Y", "1 año")]
    [InlineData("P3Y", "3 años")]
    [InlineData("P6M", "6 meses")]
    [InlineData("P1M", "1 mes")]
    [InlineData("P1Y2M15D", "1 año, 2 meses, 15 días")]
    [InlineData("P5Y10D", "5 años, 10 días")]
    [InlineData("P2Y3M", "2 años, 3 meses")]
    [InlineData(null, "-")]
    [InlineData("", "-")]
    [InlineData("   ", "-")]
    [InlineData("Texto Libre", "Texto Libre")]
    public void FormatearAntiguedad_ConvierteCorrectamente(string? input, string esperado)
    {
        var resultado = PdfBuilder.FormatearAntiguedad(input);
        Assert.Equal(esperado, resultado);
    }
}
