namespace ReportGeneratorUtils
{
    using System.Collections.Generic;

    /// <summary>
    /// Report builder base class
    /// </summary>
    /// <seealso cref="ReportGeneratorUtils.IReportBuilder" />
    public abstract class ReportBuilderBase : IReportBuilder
    {

        protected virtual IList<IReportPart> ReportParts { get; private set; } = new List<IReportPart>();

        public void AppendReportSection<T>(ReportSectionDisplayType contentType, IList<T> contents, string sectionHeader, string sectionFooter)
        {
            this.ReportParts.Add(this.GetReportPart(contentType, contents, sectionHeader, sectionFooter));
        }

        protected virtual IReportPart GetReportPart<T>(
            ReportSectionDisplayType contentType,
            IList<T> contents,
            string sectionHeader,
            string sectionFooter)
        {
            var reportPartInstance = new ReportPart(contentType, sectionHeader, sectionFooter);

            foreach (var reportContent in contents)
            {
                reportPartInstance.Parts.Add(reportContent);
            }

            return reportPartInstance;
        }

        public abstract string Build(string reportTitle, string reportSubTitle, string reportFooter);
    }
}
