using ReportGeneratorUtils;
using ReportGeneratorUtils.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportGenApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            GenerateAndShowReport(ReportSectionDisplayType.Table);
        }

        private void GenerateAndShowReport(ReportSectionDisplayType reportPartType)
        {
            // Create the objects collection that are going to be presented in the report


            /* Create the report contents with the type of display.
             * 
             * Table: to display the collection as a HTML table in the report
             * Label: to display the collection as a HTML label in the report
             * Paragraph: to display the collection as a HTML paragraph in the report
             * */

            // Generate the report
            IReportBuilder reportGenerator = new HtmlReportBuilder();
            reportGenerator.AppendReportSection(
                reportPartType,
                this.GetReportData(),
                "Employee list",
                "You are truely appreciated for all your effort in each day.");

            var htmlReport = reportGenerator.Build(
                "Employee list",
                "Below is the list of our employees.",
                "Copyright © MyCompany");

            this.ShowReport(htmlReport);
            this.SaveReport(htmlReport);
        }

        private IList<Employee> GetReportData()
        {
            return new List<Employee>
            {
                new Employee(1, "Satheesh Krishnasamy."),
                new Employee(2, "Martin Fowler")
            };
        }

        private IList<Employee> GetReportLargeData(int requiredDataCount)
        {
            var employees = new List<Employee>
            {
                new Employee(1, "Satheesh Krishnasamy."),
                new Employee(2, "Martin Fowler")
            };

            var targetEmployees = new List<Employee>();
            var index = 0;
            for (int i = 0; i < requiredDataCount; i++)
            {
                if (!currentToken.IsCancellationRequested)
                {
                    targetEmployees.Add(employees[index]);
                    if (index == 0)
                        index = 1;
                    else
                        index = 0;
                }
            }

            return targetEmployees;
        }

        private void ShowReport(string reportContent)
        {
            txtXmlReport.Text = reportContent;
        }

        private void SaveReport(string reportContent)
        {
            // save the file
            var reportFilePath = $"Reports\\HTMLReport_{DateTime.Now.ToString("yyyyMMddhhmmss")}.html";
            IReportSaver saver = new ReportFileSaver();
            saver.SaveReport(Path.Combine(Environment.CurrentDirectory, reportFilePath), reportContent, true);
            OpenFolderInWindowsExplorer(reportFilePath, "");
        }

        private void OpenFolderInWindowsExplorer(string path, string args)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(args))
                    args = string.Empty;

                System.Diagnostics.Process.Start(path, args);
            }
            catch (Exception excep)
            {
                MessageBox.Show("There was an error in opening your report. " + excep.Message);
            }
        }

        private void BtnParaReport_Click(object sender, EventArgs e)
        {
            GenerateAndShowReport(ReportSectionDisplayType.Paragraph);
        }

        private void BtnShowLabelReport_Click(object sender, EventArgs e)
        {
            GenerateAndShowReport(ReportSectionDisplayType.Label);
        }

        private void BtnMultiPartReport_Click(object sender, EventArgs e)
        {
            IObjectToXmlConverterFactory factory = new ObjectToXmlConverterFactory();
            IReportBuilder reportGenerator = new HtmlReportBuilder(factory);

            // Create the objects collection that are going to be presented in the report
            IList<Blogger> bloggersCollection = new List<Blogger>
            {
                new Blogger(1, "Satheesh Krishnasamy."),
                new Blogger(2, "Martin Fowler")
            };



            /* Create the report contents with the type of display.
             * 
             * Table: to display the collection as a HTML table in the report
             * Label: to display the collection as a HTML label in the report
             * Paragraph: to display the collection as a HTML paragraph in the report
             * */
            reportGenerator.AppendReportSection(
                ReportSectionDisplayType.Table,
                bloggersCollection,
                "Bloggers list",
                "You are truely appreciated for all your effort in each day. Wish you all thebest !!!!!",
                currentToken);


            // Create the objects collection that are going to be presented in the report
            IList<Article> articleCollection = new List<Article>
            {
                new Article("How safe is the anti-forgery token?", "This is a wonderful article about the anit-forgery token.This is a wonderful article about the anit-forgery token.This is a wonderful article about the anit-forgery token.This is a wonderful article about the anit-forgery token.This is a wonderful article about the anit-forgery token.This is a wonderful article about the anit-forgery token.This is a wonderful article about the anit-forgery token.This is a wonderful article about the anit-forgery token.This is a wonderful article about the anit-forgery token.This is a wonderful article about the anit-forgery token.This is a wonderful article about the anit-forgery token."),
                new Article("How safe is the viewstate field?", "This is a wonderful article about the viewstate field in asp.net web forms. This is a wonderful article about the viewstate field in asp.net web forms. This is a wonderful article about the viewstate field in asp.net web forms. This is a wonderful article about the viewstate field in asp.net web forms. This is a wonderful article about the viewstate field in asp.net web forms. This is a wonderful article about the viewstate field in asp.net web forms. This is a wonderful article about the viewstate field in asp.net web forms. This is a wonderful article about the viewstate field in asp.net web forms. This is a wonderful article about the viewstate field in asp.net web forms. This is a wonderful article about the viewstate field in asp.net web forms. ")
            };


            // Generate the report


            reportGenerator.AppendReportSection(ReportSectionDisplayType.Paragraph,
                articleCollection,
                "Article published today",
                DateTime.Now.ToLongTimeString());

            reportGenerator.AppendReportSection(ReportSectionDisplayType.Label,
                articleCollection,
                "Article published today",
                DateTime.Now.ToLongTimeString());

            var htmlReport = txtXmlReport.Text = reportGenerator.Build("My blog", "Articles published today.", "Copyright © MyCompany");
            // save the file
            var reportFilePath = $"Reports\\HTMLReport_{DateTime.Now.ToString("yyyyMMddhhmmss")}.html";
            IReportSaver saver = new ReportFileSaver();
            saver.SaveReport(Path.Combine(Environment.CurrentDirectory, reportFilePath), htmlReport, true);
            OpenFolderInWindowsExplorer(reportFilePath, "");
        }

        private bool isLargeReportIsInProgress = false;
        private CancellationTokenSource tokenSource = new CancellationTokenSource();
        private CancellationToken currentToken = CancellationToken.None;
        private async void BtnBigReport_ClickAsync(object sender, EventArgs e)
        {
            if (isLargeReportIsInProgress)
            {
                btnBigReport.Text = "Cancelling..";
                tokenSource.Cancel();
            }
            else
            {

                if (!int.TryParse(txtLargeReportElementCount.Text, out int reportElementCount))
                    reportElementCount = 10000;

                txtLargeReportElementCount.Text = reportElementCount.ToString();
                btnBigReport.Text = "Cancel Large Report";
                isLargeReportIsInProgress = true;
                tokenSource = new CancellationTokenSource();
                currentToken = tokenSource.Token;

                /* Callback when some progress is reported by the report generation task 
                 * which will be running in another thread
                 * */
                IProgress<string> progress = new Progress<string>(htmlReport =>
                {
                    if (!currentToken.IsCancellationRequested)
                        this.ShowReport(htmlReport);

                    if (!currentToken.IsCancellationRequested)
                        this.SaveReport(htmlReport);

                    isLargeReportIsInProgress = false;
                    btnBigReport.Text = "Large Report";
                });

                // Run the report generation task
                await Task.Factory.StartNew(async () =>
                 {
                     var report = await this.GenerateLargeReportAsync(ReportSectionDisplayType.Table, reportElementCount);

                     // report the progress
                     progress.Report(report);
                 });
            }
        }

        private async Task<string> GenerateLargeReportAsync(ReportSectionDisplayType reportPartType, int count)
        {
            // Create the objects collection that are going to be presented in the report


            /* Create the report contents with the type of display.
             * 
             * Table: to display the collection as a HTML table in the report
             * Label: to display the collection as a HTML label in the report
             * Paragraph: to display the collection as a HTML paragraph in the report
             * */

            // Demo purpose only. Wait for 10 seconds.
            await Task.Delay(10000);

            // Generate the report
            IReportBuilder reportGenerator = new HtmlReportBuilder();
            reportGenerator.AppendReportSection(
                reportPartType,
                this.GetReportLargeData(count),
                "Employee list",
                "You are truely appreciated for all your effort in each day.");

            var htmlReport = reportGenerator.Build(
                "Employee list",
                "Below is the list of our employees.",
                "Copyright © MyCompany",
                currentToken);

            return htmlReport;
        }
    }
    /// <summary>
    /// Employee class
    /// </summary>
    public class Employee
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Employee"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        public Employee(int id, string name)
        {
            this.EmployeeID = id;
            this.EmployeeName = name;
        }

        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        /// <value>
        /// The employee identifier.
        /// </value>
        [ReportDisplay("Employee ID")]
        public int EmployeeID { get; set; }

        /// <summary>
        /// Gets or sets the name of the employee.
        /// </summary>
        /// <value>
        /// The name of the employee.
        /// </value>
        [ReportDisplay("Employee Name")]
        public string EmployeeName { get; set; }
    }

    public class Blogger
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Employee"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        public Blogger(int id, string name)
        {
            this.EmployeeID = id;
            this.EmployeeName = name;
        }

        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        /// <value>
        /// The employee identifier.
        /// </value>
        [ReportDisplay("Blogger ID")]
        public int EmployeeID { get; set; }

        /// <summary>
        /// Gets or sets the name of the employee.
        /// </summary>
        /// <vaue>
        /// The name of the employee.
        /// </value>
        [ReportDisplay("Blogger Name")]
        public string EmployeeName { get; set; }
    }

    public class Article
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Employee"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        public Article(string title, string content)
        {
            this.Title = title;
            this.Content = content;
        }

        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        /// <value>
        /// The employee identifier.
        /// </value>
        [ReportDisplay("Title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the name of the employee.
        /// </summary>
        /// <value>
        /// The name of the employee.
        /// </value>
        [ReportDisplay("Content")]
        public string Content { get; set; }
    }
}
