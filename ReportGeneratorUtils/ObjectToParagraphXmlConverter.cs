namespace ReportGeneratorUtils
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading;

    internal sealed class ObjectToParagraphXmlConverter : ObjectToXmlConverterBase
    {
        public override void ConvertToXml(ref StringBuilder sb, IReportPart reportContentItem, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(reportContentItem.GroupHeader))
            {
                sb.Append($"<label>{GetHtmlEncodedString(reportContentItem.GroupHeader)}</label>");
            }

            foreach (var part in reportContentItem.Parts)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                RenderParagraph(sb, part, cancellationToken);
            }

            if (!string.IsNullOrWhiteSpace(reportContentItem.GroupFooter))
            {
                sb.Append($"<label>{GetHtmlEncodedString(reportContentItem.GroupFooter)}</label>");
            }
        }

        public override void ConvertToXml(ref StringBuilder sb, IReportPart reportContentItem)
        {
            this.ConvertToXml(ref sb, reportContentItem, CancellationToken.None);
        }

        private void RenderParagraph(StringBuilder sb, object bodyitem, CancellationToken cancellationToken)
        {
            if (bodyitem != null)
            {
                Type t = bodyitem.GetType();
                foreach (var prop in t.GetProperties())
                {
                    if (cancellationToken.IsCancellationRequested)
                        return;

                    var attr = prop.GetCustomAttributes(typeof(ReportDisplayAttribute), true)
                        .Cast<ReportDisplayAttribute>()
                        .FirstOrDefault();

                    if (attr != null)
                    {
                        var attrValue = attr.DisplayText;
                        var propValue = prop.GetValue(bodyitem);
                        if (propValue != null)
                        {
                            sb.Append($"<Paragraph SubTitle='{GetHtmlEncodedString(attr.DisplayText)}'>{GetHtmlEncodedString(Convert.ToString(propValue))}</Paragraph>");
                        }
                    }
                }
            }
        }
    }
}
