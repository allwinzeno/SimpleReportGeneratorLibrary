namespace ReportGeneratorUtils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal sealed class ObjectToTableXmlConverter : ObjectToXmlConverterBase
    {

        private void renderTableHeader(StringBuilder sb, object reportContentItem)
        {
            if (reportContentItem != null)
            {
                Type t = reportContentItem.GetType();
                foreach (var prop in t.GetProperties())
                {
                    var attr = prop.GetCustomAttributes(typeof(ReportDisplayAttribute), true)
                        .Cast<ReportDisplayAttribute>()
                        .FirstOrDefault();

                    if (attr != null)
                    {
                        var attrValue = attr.DisplayText;
                        sb.Append($"<col name='{GetEncodedString(attrValue)}'></col>");
                    }
                }
            }
        }

        private void renderTableRow(StringBuilder sb, object p)
        {
            if (p != null)
            {
                sb.Append(@"<row>");
                Type t = p.GetType();
                foreach (var prop in t.GetProperties())
                {
                    var attr = prop.GetCustomAttributes(typeof(ReportDisplayAttribute), true).Cast<ReportDisplayAttribute>().FirstOrDefault();
                    if (attr != null)
                    {
                        var PropValue = prop.GetValue(p, null);
                        sb.Append($"<col>{GetEncodedString(Convert.ToString(PropValue))}</col>");
                    }
                }
                sb.Append("</row>");
            }
        }

        private void renderTable(StringBuilder sb, IReportPart reportSection)
        {
            if (reportSection.ReportPartType == ContentType.Table)
            {

                IEnumerable<object> table = reportSection.Parts;

                if (table != null && table.Count() > 0)
                {
                    sb.Append($"<table title='{GetEncodedString(reportSection.GroupHeader)}'>");

                    renderTableHeader(sb, table.ElementAt(0));
                    foreach (var row in table)
                    {
                        renderTableRow(sb, row);
                    }

                    sb.Append("</table>");

                    if (!string.IsNullOrWhiteSpace(reportSection.GroupFooter))
                    {
                        sb.Append($"<table footer='{GetEncodedString(reportSection.GroupFooter)}'></table>");
                    }
                }

            }
        }

        public override void ConvertToXml(ref StringBuilder sb, IReportPart reportContentItem)
        {
            this.renderTable(sb, reportContentItem);
        }
    }
}
