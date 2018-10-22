namespace ReportGeneratorUtils
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading;

    internal sealed class ObjectToLabelXmlConverter : ObjectToXmlConverterBase
    {
        public override void ConvertToXml(ref StringBuilder sb, IReportPart reportContentItem)
        {
            this.ConvertToXml(ref sb, reportContentItem, CancellationToken.None);
        }

        public override void ConvertToXml(ref StringBuilder sb, IReportPart reportContentItem, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return;

            if (!string.IsNullOrWhiteSpace(reportContentItem.GroupHeader))
            {
                sb.Append($"<label>{GetHtmlEncodedString(reportContentItem.GroupHeader)}</label>");
            }

            foreach (var part in reportContentItem.Parts)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                RenderLabel(sb, part);
            }

            if (cancellationToken.IsCancellationRequested)
                return;

            if (!string.IsNullOrWhiteSpace(reportContentItem.GroupFooter))
            {
                sb.Append($"<label>{GetHtmlEncodedString(reportContentItem.GroupFooter)}</label>");
            }
        }

        private void RenderLabel(StringBuilder sb, object bodyitem)
        {
            if (bodyitem != null)
            {
                Type t = bodyitem.GetType();
                foreach (var prop in t.GetProperties())
                {
                    var attr = prop.GetCustomAttributes(typeof(ReportDisplayAttribute), true)
                        .Cast<ReportDisplayAttribute>()
                        .FirstOrDefault();

                    if (attr != null)
                    {
                        var attrValue = attr.DisplayText;
                        var propValue = prop.GetValue(bodyitem);
                        if (propValue != null)
                        {
                            sb.Append($"<label name='{GetHtmlEncodedString(attr.DisplayText)}'> {GetHtmlEncodedString(Convert.ToString(propValue))}</label>");
                        }
                    }
                }
            }
        }
    }
}
