namespace ReportGeneratorUtils
{
    using System;
    using System.Linq;
    using System.Text;

    internal sealed class ObjectToLabelXmlConverter : ObjectToXmlConverterBase
    {
        public override void ConvertToXml(ref StringBuilder sb, IReportPart reportContentItem)
        {
            if (!string.IsNullOrWhiteSpace(reportContentItem.GroupHeader))
            {
                sb.Append($"<label>{GetEncodedString(reportContentItem.GroupHeader)}</label>");
            }

            foreach (var part in reportContentItem.Parts)
            {
                renderLabel(sb, part);
            }

            if (!string.IsNullOrWhiteSpace(reportContentItem.GroupFooter))
            {
                sb.Append($"<label>{GetEncodedString(reportContentItem.GroupFooter)}</label>");
            }
        }

        private void renderLabel(StringBuilder sb, object bodyitem)
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
                            sb.Append($"<label name='{GetEncodedString(attr.DisplayText)}'> {GetEncodedString(Convert.ToString(propValue))}</label>");
                        }
                    }
                }
            }
        }
    }
}
