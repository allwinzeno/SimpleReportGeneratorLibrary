namespace ReportGeneratorUtils
{
    using System;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// Interface provides methods to be implemented to support object to xml conversion
    /// </summary>
    public interface IObjectToXmlConverter
    {
        /// <summary>
        /// Converts to XML.
        /// Note: This is marked as obsolete method.
        /// </summary>
        /// <param name="resultContainer">The string builder instance.</param>
        /// <param name="reportContentItem">The report content item.</param>
        [Obsolete("Try using the another method with the same name. If you do not have the CancellationToken then pass its default value (CancellationToken.None) to the another method.")]
        void ConvertToXml(ref StringBuilder resultContainer, IReportPart reportContentItem);

        /// <summary>
        /// Converts to XML.
        /// </summary>
        /// <param name="resultContainer">The result container.</param>
        /// <param name="reportContentItem">The report content item.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        void ConvertToXml(ref StringBuilder resultContainer, IReportPart reportContentItem, CancellationToken cancellationToken);
    }
}
