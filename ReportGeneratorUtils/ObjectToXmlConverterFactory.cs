namespace ReportGeneratorUtils
{
    using System;

    public class ObjectToXmlConverterFactory : IObjectToXmlConverterFactory
    {
        public IObjectToXmlConverter GetObjectToXmlConverter(ContentType targetContentType)
        {
            switch (targetContentType)
            {
                case ContentType.Table:
                    return new ObjectToTableXmlConverter();

                case ContentType.Label:
                    return new ObjectToLabelXmlConverter();
                case ContentType.Paragraph:
                    return new ObjectToParagraphXmlConverter();
                default:
                    throw new Exception($"{targetContentType} - The content type is not supported.");

            }
        }
    }
}
