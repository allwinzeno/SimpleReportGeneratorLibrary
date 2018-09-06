namespace ReportGeneratorUtils
{
    using System.Collections.Generic;

    public interface IReportPart
    {
        string GroupHeader { get; }
        IList<object> Parts { get; }
        string GroupFooter { get; }
        ContentType ReportPartType { get; }
    }
}
