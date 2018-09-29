using System.Threading;

namespace ReportGeneratorUtils
{
    /// <summary>
    /// HTML Report builder class
    /// </summary>
    /// <seealso cref="ReportGeneratorUtils.ReportBuilderBase" />
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

        /// <summary>
        /// Builds the specified report title.
        /// </summary>
        /// <param name="reportTitle">The report title.</param>
        /// <param name="reportSubTitle">The report sub title.</param>
        /// <param name="reportFooter">The report footer.</param>
        /// <param name="cancellationToken">
        /// The cancellation token. The caller can cancel the report building operation using this.</param>
        /// <returns>Report based on the given input in HTML format</returns>
        public override string Build(
           string reportTitle,
           string reportSubTitle,
           string reportFooter,
           CancellationToken cancellationToken)
        {

            if (cancellationToken.IsCancellationRequested)
            {
                return string.Empty;
            }
            // form the report
            IReport rpt = new Report(reportTitle, reportSubTitle, reportFooter, this.ReportParts);


            if (cancellationToken.IsCancellationRequested)
            {
                return string.Empty;
            }
            // get the xml representation of the report object
            IReportXmlConverter objtoXmlConverter = new ReportXmlConverter(this.factory);
            var result = objtoXmlConverter.ConvertToReportXml(rpt, cancellationToken);

            if (cancellationToken.IsCancellationRequested)
            {
                return string.Empty;
            }

            // either you can use the default xslt or your own xslt file
            var htmlReport = XmlToHtmlTransformer.TransformToHTML(result, cancellationToken, this.xsltPath);
            return htmlReport;
        }

        /// <summary>
        /// Builds the specified report title.
        /// </summary>
        /// <param name="reportTitle">The report title.</param>
        /// <param name="reportSubTitle">The report sub title.</param>
        /// <param name="reportFooter">The report footer.</param>
        /// <returns>Report based on the given input in HTML format</returns>
        public override string Build(
            string reportTitle,
            string reportSubTitle,
            string reportFooter)
        {
            return this.Build(reportTitle, reportSubTitle, reportFooter, CancellationToken.None);
        }
    }
}
