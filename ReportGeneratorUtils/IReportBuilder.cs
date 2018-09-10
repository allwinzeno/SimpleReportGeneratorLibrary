namespace ReportGeneratorUtils
{
    using System.Collections.Generic;

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

        void AppendReportSection<T>(
            ReportSectionDisplayType contentType,
            IList<T> contents,
            string sectionHeader,
            string sectionFooter);
    }
}
