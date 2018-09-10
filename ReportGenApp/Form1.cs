using ReportGeneratorUtils;
using ReportGeneratorUtils.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ReportGenApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GenerateAndShowReport(ReportSectionDisplayType.Table);
        }

        private void GenerateAndShowReport(ReportSectionDisplayType reportPartType)
        {
            // Create the objects collection that are going to be presented in the report
            IList<Employee> employeesCollection = new List<Employee>();
            employeesCollection.Add(new Employee(1, "Satheesh Krishnasamy."));
            employeesCollection.Add(new Employee(2, "Martin Fowler"));

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
                employeesCollection,
                "Employee list",
                "You are truely appreciated for all your effort in each day.");

            var htmlReport = reportGenerator.Build(
                "Employee list",
                "Below is the list of our employees.",
                "Copyright © MyCompany");

            txtXmlReport.Text = htmlReport;

            // save the file
            var reportFilePath = $"Reports\\HTMLReport_{DateTime.Now.ToString("yyyyMMddhhmmss")}.html";
            IReportSaver saver = new ReportFileSaver();
            saver.SaveReport(Path.Combine(Environment.CurrentDirectory, reportFilePath), htmlReport, true);
            openFolderInWindowsExplorer(reportFilePath, "");
        }

        private void openFolderInWindowsExplorer(string path, string args)
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

        private void btnParaReport_Click(object sender, EventArgs e)
        {
            GenerateAndShowReport(ReportSectionDisplayType.Paragraph);
        }

        private void btnShowLabelReport_Click(object sender, EventArgs e)
        {
            GenerateAndShowReport(ReportSectionDisplayType.Label);
        }

        private void btnMultiPartReport_Click(object sender, EventArgs e)
        {
            IObjectToXmlConverterFactory factory = new ObjectToXmlConverterFactory();
            IReportBuilder reportGenerator = new HtmlReportBuilder(factory);
            
            // Create the objects collection that are going to be presented in the report
            IList<Blogger> bloggersCollection = new List<Blogger>();
            bloggersCollection.Add(new Blogger(1, "Satheesh Krishnasamy."));
            bloggersCollection.Add(new Blogger(2, "Martin Fowler"));



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
                "You are truely appreciated for all your effort in each day. Wish you all thebest !!!!!");


            // Create the objects collection that are going to be presented in the report
            IList<Article> articleCollection = new List<Article>();
            articleCollection.Add(new Article("How safe is the anti-forgery token?", "This is a wonderful article about the anit-forgery token.This is a wonderful article about the anit-forgery token.This is a wonderful article about the anit-forgery token.This is a wonderful article about the anit-forgery token.This is a wonderful article about the anit-forgery token.This is a wonderful article about the anit-forgery token.This is a wonderful article about the anit-forgery token.This is a wonderful article about the anit-forgery token.This is a wonderful article about the anit-forgery token.This is a wonderful article about the anit-forgery token.This is a wonderful article about the anit-forgery token."));
            articleCollection.Add(new Article("H" +
                "]ow safe is the viewstate field?", "This is a wonderful article about the viewstate field in asp.net web forms. This is a wonderful article about the viewstate field in asp.net web forms. This is a wonderful article about the viewstate field in asp.net web forms. This is a wonderful article about the viewstate field in asp.net web forms. This is a wonderful article about the viewstate field in asp.net web forms. This is a wonderful article about the viewstate field in asp.net web forms. This is a wonderful article about the viewstate field in asp.net web forms. This is a wonderful article about the viewstate field in asp.net web forms. This is a wonderful article about the viewstate field in asp.net web forms. This is a wonderful article about the viewstate field in asp.net web forms. "));


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
            openFolderInWindowsExplorer(reportFilePath, "");
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
