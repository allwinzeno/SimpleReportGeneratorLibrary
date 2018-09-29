using System.Threading;

namespace ReportGeneratorUtils
{
    public interface IReportXmlConverter
    {
        /// <summary>
        /// Converts to report XML.
        /// </summary>
        /// <param name="reportInfo">The report information.</param>
        /// <returns>Xml string representation of the report</returns>
        string ConvertToReportXml(IReport reportInfo);

        /// <summary>
        /// Converts to report XML.
        /// </summary>
        /// <param name="reportInfo">The report information.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Xml string representation of the report</returns>
        string ConvertToReportXml(IReport reportInfo, CancellationToken cancellationToken);
    }
}
