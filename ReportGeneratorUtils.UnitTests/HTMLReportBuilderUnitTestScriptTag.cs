using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using System.Threading;

namespace ReportGeneratorUtils.UnitTests
{
    /// <summary>
    /// Unit test class for testing the HtmlReportBuilder method(s)
    /// </summary>
    public partial class HtmlReportBuilderUnitTest
    {
        [TestMethod]
        public void Script_Tag_Should_NOT_Be_Rendered_When_NoScript_XSLT_Used_Test()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken currentToken = tokenSource.Token;

            // get the asked number of report data in the collection
            var reportData = this.GetReportData(10);

            var XsltFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Templates\\HTMLReportNoScript.xslt");


            // Generate the report
            var report = GenerateReportUsingHtmlReportBuilder(ReportSectionDisplayType.Table, currentToken, reportData, XsltFilePath);

            Assert.IsNotNull(report);
            Assert.IsFalse(report.Contains("<script"));
        }

        [TestMethod]
        public void Script_Tag_Should_NOT_Be_Rendered_When_NoScript_XSLT_Used_For_Label_Type_Report_Test()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken currentToken = tokenSource.Token;

            // get the asked number of report data in the collection
            var reportData = this.GetReportData(10);

            var XsltFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Templates\\HTMLReportNoScript.xslt");


            // Generate the report
            var report = GenerateReportUsingHtmlReportBuilder(ReportSectionDisplayType.Label, currentToken, reportData, XsltFilePath);

            Assert.IsNotNull(report);
            Assert.IsFalse(report.Contains("<script"));
        }

        [TestMethod]
        public void Script_Tag_Should_NOT_Be_Rendered_When_NoScript_XSLT_Used_For_Paragraph_Type_Report_Test()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken currentToken = tokenSource.Token;

            // get the asked number of report data in the collection
            var reportData = this.GetReportData(10);

            var XsltFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Templates\\HTMLReportNoScript.xslt");


            // Generate the report
            var report = GenerateReportUsingHtmlReportBuilder(ReportSectionDisplayType.Paragraph, currentToken, reportData, XsltFilePath);

            Assert.IsNotNull(report);
            Assert.IsFalse(report.Contains("<script"));
        }

        [TestMethod]
        public void Script_Tag_Should_Be_Rendered_When_Default_XSLT_Used_Test()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken currentToken = tokenSource.Token;

            // get the asked number of report data in the collection
            var reportData = this.GetReportData(10);

            // Generate the report
            var report = GenerateReportUsingHtmlReportBuilder(ReportSectionDisplayType.Table, currentToken, reportData);

            Assert.IsNotNull(report);
            Assert.IsTrue(report.Contains("<script"));
        }

        [TestMethod]
        public void Script_Tag_Should_NOT_Be_Rendered_For_Label_Type_Report_Test()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken currentToken = tokenSource.Token;

            // get the asked number of report data in the collection
            var reportData = this.GetReportData(10);

            // Generate the report
            var report = GenerateReportUsingHtmlReportBuilder(ReportSectionDisplayType.Label, currentToken, reportData);

            Assert.IsNotNull(report);
            Assert.IsFalse(report.Contains("<script"));
        }

        [TestMethod]
        public void Script_Tag_Should_NOT_Be_Rendered_For_Paragraph_Type_Report_Test()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken currentToken = tokenSource.Token;

            // get the asked number of report data in the collection
            var reportData = this.GetReportData(10);

            // Generate the report
            var report = GenerateReportUsingHtmlReportBuilder(ReportSectionDisplayType.Paragraph, currentToken, reportData);

            Assert.IsNotNull(report);
            Assert.IsFalse(report.Contains("<script"));
        }
    }
}
