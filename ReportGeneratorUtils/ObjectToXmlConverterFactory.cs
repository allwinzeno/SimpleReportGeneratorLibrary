namespace ReportGeneratorUtils
{
    using System;

    /// <summary>
    /// Default implementation of the IObjectToXmlConverterFactory.
    /// You can derive and extend this class to support more IObjectToXmlConverter converters.
    /// </summary>
    /// <seealso cref="ReportGeneratorUtils.IObjectToXmlConverterFactory" />
    public class ObjectToXmlConverterFactory : IObjectToXmlConverterFactory
    {
        public virtual IObjectToXmlConverter GetObjectToXmlConverter(ReportSectionDisplayType targetContentType)
        {
            switch (targetContentType)
            {
                case ReportSectionDisplayType.Table:
                    return new ObjectToTableXmlConverter();

                case ReportSectionDisplayType.Label:
                    return new ObjectToLabelXmlConverter();
                case ReportSectionDisplayType.Paragraph:
                    return new ObjectToParagraphXmlConverter();
                default:
                    throw new Exception($"{targetContentType} - The content type is not supported.");

            }
        }
    }
}
