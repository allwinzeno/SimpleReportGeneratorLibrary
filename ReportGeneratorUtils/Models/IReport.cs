namespace ReportGeneratorUtils
{
    using System.Collections.Generic;

    public interface IReport
    {
        string Title { get; }
        string Header { get; }
        IEnumerable<IReportPart> ReportContent { get; }
        bool ShowDefaultFooterInfo { get; }
        string Footer { get; }
    }
}
