namespace ReportGeneratorUtils
{
    public interface IObjectToXmlConverterFactory
    {
        IObjectToXmlConverter GetObjectToXmlConverter(ContentType targetContentType);
    }
}
