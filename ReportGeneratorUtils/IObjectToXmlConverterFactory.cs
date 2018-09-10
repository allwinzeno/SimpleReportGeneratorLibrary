namespace ReportGeneratorUtils
{
    public interface IObjectToXmlConverterFactory
    {
        IObjectToXmlConverter GetObjectToXmlConverter(ReportSectionDisplayType targetContentType);
    }
}
