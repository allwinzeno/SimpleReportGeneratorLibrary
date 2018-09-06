namespace ReportGeneratorUtils
{
    public interface IReportXmlConverter
    {
        string ConvertToReportXml(IReport reportInfo);
    }
}
