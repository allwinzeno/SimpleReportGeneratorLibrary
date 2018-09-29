namespace ReportGeneratorUtils
{
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// Report builder
    /// </summary>
    public interface IReportBuilder
    {
        /// <summary>
        /// Generates the specified report title.
        /// </summary>
        /// <param name="reportTitle">The report title.</param>
        /// <param name="reportSubTitle">The report sub title.</param>
        /// <param name="reportFooter">The report footer.</param>
        /// <returns>
        /// Report in the string format
        /// </returns>
        string Build(
            string reportTitle,
            string reportSubTitle,
            string reportFooter);

        /// <summary>
        /// Generates the specified report title.
        /// </summary>
        /// <param name="reportTitle">The report title.</param>
        /// <param name="reportSubTitle">The report sub title.</param>
        /// <param name="reportFooter">The report footer.</param>
        /// <param name="cancellationtoken">The cancellationtoken.</param>
        /// <returns>
        /// Report in the string format
        /// </returns>
        string Build(
            string reportTitle,
            string reportSubTitle,
            string reportFooter,
            CancellationToken cancellationtoken);

        /// <summary>
        /// Appends the report section.
        /// </summary>
        /// <typeparam name="T">Datatype of the report content</typeparam>
        /// <param name="contentType">Report section content.</param>
        /// <param name="contents">The contents collection.</param>
        /// <param name="sectionHeader">The section header.</param>
        /// <param name="sectionFooter">The section footer.</param>
        void AppendReportSection<T>(
            ReportSectionDisplayType contentType,
            IList<T> contents,
            string sectionHeader,
            string sectionFooter);

        /// <summary>
        /// Appends the report section.
        /// </summary>
        /// <typeparam name="T">Datatype of the report content</typeparam>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="sectionHeader">The section header.</param>
        /// <param name="sectionFooter">The section footer.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        void AppendReportSection<T>(
            ReportSectionDisplayType contentType,
            IList<T> contents,
            string sectionHeader,
            string sectionFooter,
            CancellationToken cancellationToken);
    }
}
