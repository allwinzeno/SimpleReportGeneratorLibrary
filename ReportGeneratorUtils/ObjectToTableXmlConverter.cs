namespace ReportGeneratorUtils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// Object to XML converter
    /// </summary>
    /// <seealso cref="ReportGeneratorUtils.ObjectToXmlConverterBase" />
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
                        sb.Append($"<col name='{GetHtmlEncodedString(attrValue)}'></col>");
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
                        sb.Append($"<col>{GetHtmlEncodedString(Convert.ToString(PropValue))}</col>");
                    }
                }
                sb.Append("</row>");
            }
        }

        private void renderTable(StringBuilder resultStringBuilder, IReportPart reportSection, CancellationToken cancellationToken)
        {
            if (reportSection.ReportPartType == ReportSectionDisplayType.Table)
            {

                IEnumerable<object> table = reportSection.Parts;

                if (table != null && table.Count() > 0)
                {
                    resultStringBuilder.Append($"<table title='{GetHtmlEncodedString(reportSection.GroupHeader)}' ");

                    if (!string.IsNullOrWhiteSpace(reportSection.GroupFooter))
                    {
                        resultStringBuilder.Append($" footer='{GetHtmlEncodedString(reportSection.GroupFooter)}' >");
                    }
                    else
                    {
                        resultStringBuilder.Append(" >");
                    }

                    renderTableHeader(resultStringBuilder, table.ElementAt(0));
                    foreach (var row in table)
                    {
                       
                        if(cancellationToken.IsCancellationRequested)
                        {
                            resultStringBuilder.Clear();
                            return;
                        }

                        renderTableRow(resultStringBuilder, row);
                    }

                    resultStringBuilder.Append("</table>");
                }

            }
        }

        /// <summary>
        /// Converts to XML.
        /// </summary>
        /// <param name="resultStringBuilder">The result string builder.</param>
        /// <param name="reportContentItem">The report content item.</param>
        public override void ConvertToXml(ref StringBuilder resultStringBuilder, IReportPart reportContentItem)
        {
            this.ConvertToXml(ref resultStringBuilder, reportContentItem, CancellationToken.None);
        }

        /// <summary>
        /// Converts to XML.
        /// </summary>
        /// <param name="resultStringBuilder">The result string builder.</param>
        /// <param name="reportContentItem">The report content item.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public override void ConvertToXml(ref StringBuilder resultStringBuilder, IReportPart reportContentItem, CancellationToken cancellationToken)
        {
            this.renderTable(resultStringBuilder, reportContentItem, cancellationToken);
        }
    }
}
