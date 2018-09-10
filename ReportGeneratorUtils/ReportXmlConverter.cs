namespace ReportGeneratorUtils
{
    using System;
    using System.Text;

    public sealed class ReportXmlConverter : IReportXmlConverter
    {
        private readonly IObjectToXmlConverterFactory factory;

        public ReportXmlConverter(IObjectToXmlConverterFactory factory)
        {
            this.factory = factory;
        }

        public string ConvertToReportXml(IReport reportInfo)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"<ReviewReport Title='{ObjectToXmlConverterBase.GetHtmlEncodedString(reportInfo.Title)}'>");
            sb.Append($"<Header>{ObjectToXmlConverterBase.GetHtmlEncodedString(reportInfo.Header)}</Header>");

            foreach (var bodyitem in reportInfo.ReportContent)
            {
                factory.GetObjectToXmlConverter(bodyitem.ReportPartType).ConvertToXml(ref sb, bodyitem);
            }

            sb.Append($"<Footer>{ObjectToXmlConverterBase.GetHtmlEncodedString(reportInfo.Footer)}</Footer>");

            if (reportInfo.ShowDefaultFooterInfo)
            {
                sb.Append("<Footer> Report generated on: " + DateTime.Now.ToString() + "</Footer>");
            }
            sb.Append("</ReviewReport>");

            return sb.ToString();
        }
    }
}

