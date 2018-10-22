using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ReportGeneratorUtils.UnitTests
{
    [TestClass]
    public partial class HtmlReportBuilderUnitTest
    {
        [TestMethod]
        public void Report_Should_Be_Generated_When_Cancellation_Is_Not_Requested()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken currentToken = tokenSource.Token;

            var reportGenerationTask = GenerateReportWithAutoGenDataAsync(ReportSectionDisplayType.Table,
                100000,
                currentToken,
                0);

            Task.WaitAll(reportGenerationTask);
            Assert.IsNotNull(reportGenerationTask.Result);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(reportGenerationTask.Result));
        }

        [TestMethod]
        public void Report_Should_NOT_Generated_When_Cancellation_Is_Requested()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken currentToken = tokenSource.Token;

            var reportGenerationTask = GenerateReportWithAutoGenDataAsync(ReportSectionDisplayType.Table,
                100000,
                currentToken,
                10000);

            tokenSource.Cancel();
            Task.WaitAll(reportGenerationTask);
            Assert.IsTrue(string.IsNullOrWhiteSpace(reportGenerationTask.Result));
        }


        private async Task<string> GenerateReportWithAutoGenDataAsync(
            ReportSectionDisplayType reportPartType,
            int count,
            CancellationToken cancellationToken,
            int delayToStart = 0)
        {
            // Wait for asked number of seconds.
            await Task.Delay(delayToStart);

            // get the asked number of report data in the collection
            var reportData = this.GetReportData(count);

            // Generate the report
            return GenerateReportUsingHtmlReportBuilder(reportPartType, cancellationToken, reportData);
        }

        private string GenerateReportUsingHtmlReportBuilder(
            ReportSectionDisplayType reportPartType,
            CancellationToken cancellationToken,
            IList<Employee> reportData,
            string xsltPath = null)
        {

            // Generate the report
            IReportBuilder reportGenerator;

            if (string.IsNullOrWhiteSpace(xsltPath))
            {
                reportGenerator = new HtmlReportBuilder();

            }
            else
            {
                reportGenerator = new HtmlReportBuilder(xsltPath);
            }

            reportGenerator.AppendReportSection(
                    reportPartType,
                    reportData,
                    "Employee list",
                    "You are truely appreciated for all your effort in each day.",
                    cancellationToken);
            var htmlReport = reportGenerator.Build(
                "Employee list",
                "Below is the list of our employees.",
                "Copyright © MyCompany",
                cancellationToken);

            return htmlReport;
        }

        private IList<Employee> GetReportData(int requiredDataCount)
        {
            var targetEmployees = new List<Employee>();
            for (int i = 0; i < requiredDataCount; i++)
            {
                targetEmployees.Add(new Employee(i + 1, "Employee name" + i + 1));
            }

            return targetEmployees;
        }
    }
}
