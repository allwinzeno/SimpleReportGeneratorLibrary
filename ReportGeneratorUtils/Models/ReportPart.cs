namespace ReportGeneratorUtils
{
    using System.Collections.Generic;

    public class ReportPart : IReportPart
    {
        public ReportPart(
            ContentType reportPartType,
            string groupHeader,
            string groupFooter)
        {
            this.ReportPartType = reportPartType;
            this.GroupHeader = groupHeader;
            this.GroupFooter = groupFooter;
        }

        public string GroupHeader { get; }
        public IList<object> Parts { get; } = new List<object>();
        public string GroupFooter { get; }
        public ContentType ReportPartType { get; }
    }
}
