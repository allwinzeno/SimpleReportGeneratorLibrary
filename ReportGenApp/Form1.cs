using ReportGeneratorUtils;
using ReportGenTestApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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

        private void button1_Click(object sender, EventArgs e)
        {

            // Create the objects collection that are going to be presented in the report
            IList<Employee> employeesCollection = new List<Employee>();
            employeesCollection.Add(new Employee(1, "Satheesh"));
            employeesCollection.Add(new Employee(2, "Martin Fowler"));

            /* Create the report contents with the type of display.
             * 
             * Table: to display the collection as a HTML table in the report
             * Label: to display the collection as a HTML label in the report
             * Paragraph: to display the collection as a HTML paragraph in the report
             * */
            var rptPartTable = new ReportPart(ContentType.Table, "All employees", "Footer info");
            var rptPartPara = new ReportPart(ContentType.Paragraph, "All employees", "Footer info");
            var rptPartLabel = new ReportPart(ContentType.Label, "All employees", "Footer info");

            foreach (var employeeObject in employeesCollection)
            {
                rptPartTable.Parts.Add(employeeObject);
                rptPartPara.Parts.Add(employeeObject);
                rptPartLabel.Parts.Add(employeeObject);
            }

            IList<IReportPart> parts = new List<IReportPart>()
            {
                rptPartTable,
                rptPartPara,
                rptPartLabel
            };

            // form the report
            IReport rpt = new Report("This is my title", "All employees:", "these are all our employees", parts);

            // create the factory that takes care of creating the xml converters
            IObjectToXmlConverterFactory factory = new ObjectToXmlConverterFactory();

            // get the xml representation of the report object
            IReportXmlConverter objtoXmlConverter = new ReportXmlConverter(factory);
            var result = objtoXmlConverter.ConvertToReportXml(rpt);

            // show the converted xml in the text box
            txtXmlReport.Text = result;

            // either you can use the default xslt or your own xslt file
            var htmlReport = XmlToHtmlTransformer.TransformToHTML(result /*, Path.Combine(Environment.CurrentDirectory, "HTMLReport.xslt")*/);

            // save the file
            var reportFilePath = $"HTMLReport_{DateTime.Now.ToString("yyyyMMddhhmmss")}.html";
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

            }
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
}
