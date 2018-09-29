namespace ReportGeneratorUtils
{
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// Report builder base class
    /// </summary>
    /// <seealso cref="ReportGeneratorUtils.IReportBuilder" />
    public abstract class ReportBuilderBase : IReportBuilder
    {
        protected virtual IList<IReportPart> ReportParts { get; private set; } = new List<IReportPart>();

        /// <summary>
        /// Appends the report section.
        /// </summary>
        /// <typeparam name="T">Data Type of the report content</typeparam>
        /// <param name="contentType">Report section content.</param>
        /// <param name="contents">The contents collection.</param>
        /// <param name="sectionHeader">The section header.</param>
        /// <param name="sectionFooter">The section footer.</param>
        public void AppendReportSection<T>(ReportSectionDisplayType contentType, IList<T> contents, string sectionHeader, string sectionFooter)
        {
            this.AppendReportSection(contentType, contents, sectionHeader, sectionFooter, CancellationToken.None);
        }

        /// <summary>
        /// Appends the report section.
        /// </summary>
        /// <typeparam name="T">Data Type of the report content</typeparam>
        /// <param name="contentType">Report section content.</param>
        /// <param name="contents">The contents collection.</param>
        /// <param name="sectionHeader">The section header.</param>
        /// <param name="sectionFooter">The section footer.</param>
        public void AppendReportSection<T>(ReportSectionDisplayType contentType, IList<T> contents, string sectionHeader, string sectionFooter, CancellationToken cancellationToken)
        {
            var reportParts = this.GetReportPart(contentType, contents, sectionHeader, sectionFooter, cancellationToken);

            if (cancellationToken.IsCancellationRequested)
                return;

            this.ReportParts.Add(reportParts);
        }

        /// <summary>
        /// Gets the report part.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="sectionHeader">The section header.</param>
        /// <param name="sectionFooter">The section footer.</param>
        /// <returns>IReport instance</returns>
        protected virtual IReportPart GetReportPart<T>(
            ReportSectionDisplayType contentType,
            IList<T> contents,
            string sectionHeader,
            string sectionFooter,
            CancellationToken cancellationToken)
        {
            var reportPartInstance = new ReportPart(contentType, sectionHeader, sectionFooter);

            foreach (var reportContent in contents)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return reportPartInstance;
                }

                reportPartInstance.Parts.Add(reportContent);
            }

            return reportPartInstance;
        }

        /// <summary>
        /// Generates the specified report title.
        /// </summary>
        /// <param name="reportTitle">The report title.</param>
        /// <param name="reportSubTitle">The report sub title.</param>
        /// <param name="reportFooter">The report footer.</param>
        /// <returns>
        /// Report in the string format
        /// </returns>
        public abstract string Build(string reportTitle, string reportSubTitle, string reportFooter);

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
        public abstract string Build(string reportTitle, string reportSubTitle, string reportFooter, CancellationToken cancellationtoken);
    }
}
