namespace ReportGeneratorUtils
{
    using System.Collections.Generic;

    public class Report : IReport
    {
        private readonly IList<IReportPart> parts = new List<IReportPart>();

        public Report(string title, string sideHeader, string footer, IEnumerable<IReportPart> reportParts)
        {
            this.Title = title;
            this.Header = sideHeader;
            this.Footer = footer;
            foreach (var part in reportParts)
            {
                this.parts.Add(part);
            }
        }

        public string Title { get; }
        public string Header { get; }
        public IEnumerable<IReportPart> ReportContent
        {
            get
            {

                return parts;
            }
        }

        public bool ShowDefaultFooterInfo
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.Footer);
            }
        }
        public string Footer { get; }
    }
}
