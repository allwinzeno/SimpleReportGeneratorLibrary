namespace ReportGeneratorUtils
{
    using System;
    using System.IO;
    using System.Reflection;
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

        /// <summary>
        /// Transforms to HTML.
        /// </summary>
        /// <param name="xmlReportData">The XML report data.</param>
        /// <param name="xsltFilePath">The XSLT file path. If not passed the default XSLT template will be used.</param>
        /// <returns>String that results after the XSLT applied on the XML string</returns>
        public static string TransformToHTML(string xmlReportData, string xsltFilePath = null)
        {
            if (string.IsNullOrWhiteSpace(xsltFilePath))
            {
                xsltFilePath = GetDefaultXsltPath();
            }

            string finalReportString = String.Empty;
            using (StringReader xsltStringFromFileReader = new StringReader(File.ReadAllText(xsltFilePath)))
            {
                // xslInput is a string that contains xsl
                using (StringReader xmlReportStringReader = new StringReader(xmlReportData)) // xmlInput is a string that contains xml
                {
                    using (XmlReader xsltFileXmlReader = XmlReader.Create(xsltStringFromFileReader))
                    {
                        XslCompiledTransform xsltCompiledTransformation = new XslCompiledTransform();
                        xsltCompiledTransformation.Load(xsltFileXmlReader);

                        using (XmlReader xmlReportXmlReader = XmlReader.Create(xmlReportStringReader))
                        {
                            using (StringWriter outputStringWriter = new StringWriter())
                            {
                                using (XmlWriter xsltXmlWriter = XmlWriter.Create(outputStringWriter, xsltCompiledTransformation.OutputSettings)) // use OutputSettings of xsl, so it can be output as HTML
                                {
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
