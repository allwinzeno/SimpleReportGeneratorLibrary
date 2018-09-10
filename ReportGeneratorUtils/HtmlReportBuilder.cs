namespace ReportGeneratorUtils
{
    public sealed class HtmlReportBuilder : ReportBuilderBase
    {
        private readonly IObjectToXmlConverterFactory factory;
        private readonly string xsltPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlReportBuilder"/> class.
        /// This constructor uses the default ObjectToXmlConverterFactory instance
        /// that is available in this library.
        /// </summary>
        /// <param name="factory">The IObjectToXmlConverterFactory factory instance. 
        /// Pass this param if you have implemented the IObjectToXmlConverterFactory interface.
        /// Or pass the ObjectToXmlConverterFactory object explicitly.
        /// </param>
        /// <param name="xsltPath">The XSLT filepath (optional). 
        /// If not passed then the default XSLT template will be applied to prepare the report.</param>
        public HtmlReportBuilder(IObjectToXmlConverterFactory factory, string xsltPath = null)
        {
            this.factory = factory;
            this.xsltPath = xsltPath;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlReportBuilder"/> class.
        /// </summary>
        /// <param name="xsltPath">The XSLT filepath (optional). 
        /// If not passed then the default XSLT template will be applied to prepare the report.</param>
        public HtmlReportBuilder(string xsltPath = null) :
            this(new ObjectToXmlConverterFactory(), xsltPath)
        {
        }

        public override string Build(
            string reportTitle,
            string reportSubTitle,
            string reportFooter)
        {
            // form the report
            IReport rpt = new Report(reportTitle, reportSubTitle, reportFooter, this.ReportParts);

            // get the xml representation of the report object
            IReportXmlConverter objtoXmlConverter = new ReportXmlConverter(this.factory);
            var result = objtoXmlConverter.ConvertToReportXml(rpt);

            // either you can use the default xslt or your own xslt file
            var htmlReport = XmlToHtmlTransformer.TransformToHTML(result, this.xsltPath);
            return htmlReport;
        }
    }
}
