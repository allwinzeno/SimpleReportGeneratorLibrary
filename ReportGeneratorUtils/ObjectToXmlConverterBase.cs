namespace ReportGeneratorUtils
{
    using System.Net;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// Base class for the Object to xml converters
    /// </summary>
    /// <seealso cref="ReportGeneratorUtils.IObjectToXmlConverter" />
    internal abstract class ObjectToXmlConverterBase : IObjectToXmlConverter
    {
        /// <summary>
        /// Converts to XML.
        /// Note: This is marked as obsolete method.
        /// </summary>
        /// <param name="resultContainer">The string builder instance.</param>
        /// <param name="reportContentItem">The report content item.</param>
        public abstract void ConvertToXml(ref StringBuilder resultContainer, IReportPart reportContentItem);

        /// <summary>
        /// Converts to XML.
        /// </summary>
        /// <param name="resultContainer">The result container.</param>
        /// <param name="reportContentItem">The report content item.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public abstract void ConvertToXml(ref StringBuilder resultContainer, IReportPart reportContentItem, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the HTML encoded string.
        /// </summary>
        /// <param name="valueToEncode">The value to encode.</param>
        /// <returns>HTML encoded string</returns>
        internal static string GetHtmlEncodedString(string valueToEncode)
        {
            return WebUtility.HtmlEncode(valueToEncode);
        }
    }
}
