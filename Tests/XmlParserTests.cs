using NubeFiscal.PdfGenerator.Services;

namespace NubeFiscal.PdfGenerator.Tests;

public class XmlParserTests
{
    [Fact]
    public void FromXml_NominaXml_ParseaCamposCorrectamente()
    {
        var xmlPath = Path.Combine(AppContext.BaseDirectory, "TestData", "nomina_sample1.xml");
        var xml = File.ReadAllText(xmlPath);

        var cfdi = CfdiXmlParser.FromXml(xml);

        Assert.Equal("5bb26c74-486d-4a35-b1c3-0b09df43fac1", cfdi.UUID);
        Assert.Equal("N", cfdi.TipoComprobante);
        Assert.Equal("CNC030224ES6", cfdi.RFCEmisor);
        Assert.Equal("VECA890311QBA", cfdi.RFCReceptor);
        Assert.Equal(6615.25m, cfdi.Total);
        Assert.Equal(12650.00m, cfdi.SubTotal);
        Assert.Equal(6034.75m, cfdi.Descuento);

        Assert.NotNull(cfdi.ComplementoNomina);
        Assert.Equal("1.2", cfdi.ComplementoNomina.Version);
        Assert.Equal("O", cfdi.ComplementoNomina.TipoNomina);
        Assert.Equal("A12B34567-9", cfdi.ComplementoNomina.RegistroPatronal);
        Assert.Equal("EICO850226HDFSRS01", cfdi.ComplementoNomina.Curp);
        Assert.Equal("P24D", cfdi.ComplementoNomina.Antiguedad);
        Assert.Equal(5, cfdi.ComplementoNomina.Percepciones.Count);
        Assert.Equal(7, cfdi.ComplementoNomina.Deducciones.Count);
    }

    [Fact]
    public void FromXml_IngresoXml_ParseaCamposCorrectamente()
    {
        var xmlPath = Path.Combine(AppContext.BaseDirectory, "TestData", "ingreso_sample.xml");
        var xml = File.ReadAllText(xmlPath);

        var cfdi = CfdiXmlParser.FromXml(xml);

        Assert.Equal("94157F28-B3B6-634A-850C-4E6B68AA3296", cfdi.UUID);
        Assert.Equal("I", cfdi.TipoComprobante);
        Assert.Equal("MONJ8610256T1", cfdi.RFCEmisor);
        Assert.Equal("SUN2407301E2", cfdi.RFCReceptor);
        Assert.Equal(10486.66m, cfdi.Total);
        Assert.Single(cfdi.Conceptos);
        Assert.Single(cfdi.Conceptos[0].Traslados);
        Assert.Equal(2, cfdi.Conceptos[0].Retenciones.Count);
    }
}
