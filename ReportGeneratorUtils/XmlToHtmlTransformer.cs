namespace ReportGeneratorUtils
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using System.Xml;
    using System.Xml.Xsl;

    public static class XmlToHtmlTransformer
    {
        private const string DEFAULT_XSLT = "Templates\\HTMLReport.xslt";

        /// <summary>
        /// Gets the default XSLT path.
        /// </summary>
        /// <returns>Default XSLT template file path</returns>
        private static string GetDefaultXsltPath()
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), DEFAULT_XSLT);
        }

        public static string TransformToHTML(string xmlReportData, string xsltFilePath = null)
        {
            return TransformToHTML(xmlReportData, CancellationToken.None, xsltFilePath);
        }

        /// <summary>
        /// Transforms to HTML.
        /// </summary>
        /// <param name="xmlReportData">The XML report data.</param>
        /// <param name="xsltFilePath">The XSLT file path. If not passed the default XSLT template will be used.</param>
        /// <returns>String that results after the XSLT applied on the XML string</returns>
        public static string TransformToHTML(string xmlReportData, CancellationToken cancellationToken, string xsltFilePath = null)
        {
            if (cancellationToken.IsCancellationRequested)
                return string.Empty;

            if (string.IsNullOrWhiteSpace(xsltFilePath))
            {
                xsltFilePath = GetDefaultXsltPath();
            }

            string finalReportString = String.Empty;
            using (StringReader xsltStringFromFileReader = new StringReader(File.ReadAllText(xsltFilePath)))
            {
                if (cancellationToken.IsCancellationRequested)
                    return string.Empty;

                // xslInput is a string that contains xsl
                using (StringReader xmlReportStringReader = new StringReader(xmlReportData)) // xmlInput is a string that contains xml
                {
                    if (cancellationToken.IsCancellationRequested)
                        return string.Empty;

                    using (XmlReader xsltFileXmlReader = XmlReader.Create(xsltStringFromFileReader))
                    {
                        if (cancellationToken.IsCancellationRequested)
                            return string.Empty;

                        XslCompiledTransform xsltCompiledTransformation = new XslCompiledTransform();
                        xsltCompiledTransformation.Load(xsltFileXmlReader);

                        using (XmlReader xmlReportXmlReader = XmlReader.Create(xmlReportStringReader))
                        {
                            if (cancellationToken.IsCancellationRequested)
                                return string.Empty;

                            using (StringWriter outputStringWriter = new StringWriter())
                            {
                                if (cancellationToken.IsCancellationRequested)
                                    return string.Empty;

                                using (XmlWriter xsltXmlWriter = XmlWriter.Create(outputStringWriter, xsltCompiledTransformation.OutputSettings)) // use OutputSettings of xsl, so it can be output as HTML
                                {
                                    if (cancellationToken.IsCancellationRequested)
                                        return string.Empty;

                                    xsltCompiledTransformation.Transform(xmlReportXmlReader, xsltXmlWriter);
                                    finalReportString = outputStringWriter.ToString();
                                }
                            }
                        }
                    }
                }
            }

            return finalReportString;
        }
    }
}
