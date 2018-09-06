namespace ReportGeneratorUtils
{
    using System.Text;

    public interface IObjectToXmlConverter
    {
        void ConvertToXml(ref StringBuilder sb, IReportPart reportContentItem);
    }
}
