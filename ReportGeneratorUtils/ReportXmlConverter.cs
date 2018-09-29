namespace ReportGeneratorUtils
{
    using System;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// Report Xml Converter
    /// </summary>
    /// <seealso cref="ReportGeneratorUtils.IReportXmlConverter" />
    public sealed class ReportXmlConverter : IReportXmlConverter
    {
        private readonly IObjectToXmlConverterFactory factory;

        public ReportXmlConverter(IObjectToXmlConverterFactory factory)
        {
            this.factory = factory;
        }

        /// <summary>
        /// Converts to report XML.
        /// </summary>
        /// <param name="reportInfo">The report information.</param>
        /// <returns>
        /// Xml string representation of the report
        /// </returns>
        public string ConvertToReportXml(IReport reportInfo)
        {
            return this.ConvertToReportXml(reportInfo, CancellationToken.None);
        }

        /// <summary>
        /// Converts to report XML.
        /// </summary>
        /// <param name="reportInfo">The report information.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// Xml string representation of the report
        /// </returns>
        public string ConvertToReportXml(IReport reportInfo, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();

            sb.Append($"<ReviewReport Title='{ObjectToXmlConverterBase.GetHtmlEncodedString(reportInfo.Title)}'>");
            sb.Append($"<Header>{ObjectToXmlConverterBase.GetHtmlEncodedString(reportInfo.Header)}</Header>");

            foreach (var bodyitem in reportInfo.ReportContent)
            {
                if(cancellationToken.IsCancellationRequested)
                {
                    return string.Empty;
                }

                factory.GetObjectToXmlConverter(bodyitem.ReportPartType).ConvertToXml(ref sb, bodyitem, cancellationToken);
            }

            sb.Append($"<Footer>{ObjectToXmlConverterBase.GetHtmlEncodedString(reportInfo.Footer)}</Footer>");

            if (cancellationToken.IsCancellationRequested)
            {
                return string.Empty;
            }

            if (reportInfo.ShowDefaultFooterInfo)
            {
                sb.Append("<Footer> Report generated on: " + DateTime.Now.ToString() + "</Footer>");
            }
            sb.Append("</ReviewReport>");

            return sb.ToString();
        }
    }
}

