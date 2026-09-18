using System.Text;

namespace NubeFiscal.PdfGenerator.Tests;

internal static class PdfTestHelpers
{
    private static readonly byte[] PdfMagicHeader = "%PDF-"u8.ToArray();

    public static void AssertValidPdf(byte[]? pdfBytes, int minSize = 1024)
    {
        Assert.NotNull(pdfBytes);
        Assert.True(pdfBytes.Length >= minSize, $"El PDF generado es demasiado pequeño ({pdfBytes.Length} bytes). Se esperaban al menos {minSize} bytes.");
        
        // Verificar que empiece con el encabezado %PDF-
        Assert.True(pdfBytes.Length >= 5, "El archivo no tiene longitud suficiente para contener el encabezado PDF.");
        for (int i = 0; i < PdfMagicHeader.Length; i++)
        {
            Assert.Equal(PdfMagicHeader[i], pdfBytes[i]);
        }
    }
}
