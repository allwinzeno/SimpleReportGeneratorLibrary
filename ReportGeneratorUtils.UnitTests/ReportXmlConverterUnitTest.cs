using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace ReportGeneratorUtils.UnitTests
{
    /// <summary>
    /// Unit test class for testing the ReportXmlConverter class method(s)
    /// </summary>
    [TestClass]
    public class ReportXmlConverterUnitTest
    {

        private IObjectToXmlConverterFactory factory;
        private string xsdFilePath;
        private string validationErrors;

        private IList<Employee> GetReportTestDataCollection()
        {
            return new List<Employee>
            {
                new Employee(1, "Satheesh"),
                new Employee(2, "Martin Fowler")
            };
        }

        private void AddCollectionToReportPart(IList<Employee> employeesCollection, IReportPart reportPart)
        {
            foreach (var employeeObject in employeesCollection)
            {
                reportPart.Parts.Add(employeeObject);
            }
        }

        [TestInitialize]
        public void TestInit()
        {
            factory = new ObjectToXmlConverterFactory();
            xsdFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Schema\\ReviewReport.xsd");
            validationErrors = string.Empty;
        }

        private void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Message))
                validationErrors += e.Message + "<br />";
        }

        [TestMethod]
        public void GeneratedXMLReportShouldMatchTheSchemaTest()
        {

            var employeesCollection = this.GetReportTestDataCollection();

            var rptPartTable = new ReportPart(ReportSectionDisplayType.Table, "All employees", "Footer info");

            AddCollectionToReportPart(employeesCollection, rptPartTable);

            IList<IReportPart> parts = new List<IReportPart>()
            {
                rptPartTable
            };


            // form the report
            IReport rpt = new Report("This is my title", "All employees:", "these are all our employees", parts);

            // get the xml representation of the report object
            IReportXmlConverter objtoXmlConverter = new ReportXmlConverter(factory);
            var result = objtoXmlConverter.ConvertToReportXml(rpt);

            try
            {
                validationErrors = string.Empty;
                XmlReader schema = XmlReader.Create(this.xsdFilePath);
                XmlDocument document = new XmlDocument();
                document.LoadXml(result);
                document.Schemas.Add(string.Empty, schema);
                ValidationEventHandler eventHandler = new ValidationEventHandler(ValidationEventHandler);
                document.Validate(eventHandler);
            }
            catch (Exception)
            {
                throw;
            }

            Console.WriteLine(validationErrors == string.Empty ? "Document is valid" : "Document invalid: " + validationErrors);

            Assert.AreEqual(validationErrors, string.Empty);
        }


        [TestMethod]
        public void ReportPart_Type_Table_GeneratedXMLReportShouldHaveNumberOfReportParts_Equals_To_The_Objects_Test()
        {
            const int PROPERTY_COUNT = 2;

            // Prepare the test data
            var employeesCollection = this.GetReportTestDataCollection();

            // form the report
            var rptPartTable = new ReportPart(ReportSectionDisplayType.Table, "All employees", "Footer info");
            AddCollectionToReportPart(employeesCollection, rptPartTable);
            IList<IReportPart> parts = new List<IReportPart>()
            {
                rptPartTable
            };
            IReport rpt = new Report("This is my title", "All employees:", "these are all our employees", parts);

            // get the xml representation of the report object
            IReportXmlConverter objtoXmlConverter = new ReportXmlConverter(factory);
            var result = objtoXmlConverter.ConvertToReportXml(rpt);

            //create xml document from string
            XDocument xdoc = XDocument.Parse(result);

            // Verification starts
            // verify the root node count
            var reportNode = xdoc.Descendants("ReviewReport");
            Assert.AreEqual(reportNode.Count(), 1);

            // verify the report part (table) count
            var tableElement = reportNode.Descendants("table");
            Assert.AreEqual(tableElement.Count(), 1);

            // verify the report header count
            Assert.AreEqual(reportNode.Descendants("Header").Count(), 1);

            /* verify the report footer count. There is a 
             * default header (report generated on) and the report header mentioned in the Report object.
             * */
            Assert.AreEqual(reportNode.Descendants("Footer").Count(), 2);

            // Verify the row count that must be equals to the no.of employees
            var rows = tableElement.Descendants("row");
            Assert.AreEqual(rows.Count(), employeesCollection.Count);

            foreach (var row in rows)
            {
                // There are 2 properties with ReportDisplay attribute. Hence there should be 2 columns.
                Assert.AreEqual(row.Descendants("col").Count(), PROPERTY_COUNT);
            }
        }

        [TestMethod]
        public void ReportPart_Type_Paragraph_GeneratedXMLReportShouldHaveNumberOfReportParts_Equals_To_The_Objects_Test()
        {
            const int PROPERTY_COUNT = 2;

            // Prepare the test data
            var employeesCollection = this.GetReportTestDataCollection();

            // form the report
            var rptPartParagraph = new ReportPart(ReportSectionDisplayType.Paragraph, "All employees", "Footer info");
            AddCollectionToReportPart(employeesCollection, rptPartParagraph);
            IList<IReportPart> parts = new List<IReportPart>()
            {
                rptPartParagraph
            };
            IReport rpt = new Report("This is my title", "All employees:", "these are all our employees", parts);

            // get the xml representation of the report object
            IReportXmlConverter objtoXmlConverter = new ReportXmlConverter(factory);
            var result = objtoXmlConverter.ConvertToReportXml(rpt);

            //create xml document from string
            XDocument xdoc = XDocument.Parse(result);

            // Verification starts
            // verify the root node count
            var reportNode = xdoc.Descendants("ReviewReport");
            Assert.AreEqual(reportNode.Count(), 1);

            // verify the report part (paragraph) count
            var paragraphElement = reportNode.Descendants("Paragraph");
            Assert.AreEqual(paragraphElement.Count(), employeesCollection.Count * PROPERTY_COUNT);

            // verify the report header count
            Assert.AreEqual(reportNode.Descendants("Header").Count(), 1);

            /* verify the report footer count. There is a 
             * default header (report generated on) and the report header mentioned in the Report object.
             * */
            Assert.AreEqual(reportNode.Descendants("Footer").Count(), 2);
        }

        [TestMethod]
        public void LabelPart_Type_Paragraph_GeneratedXMLReportShouldHaveNumberOfReportParts_Equals_To_The_Objects_Test()
        {
            const int PROPERTY_COUNT = 2;

            // Prepare the test data
            var employeesCollection = this.GetReportTestDataCollection();

            // form the report
            var rptPartParagraph = new ReportPart(ReportSectionDisplayType.Label, "All employees", "Footer info");
            AddCollectionToReportPart(employeesCollection, rptPartParagraph);
            IList<IReportPart> parts = new List<IReportPart>()
            {
                rptPartParagraph
            };
            IReport rpt = new Report("This is my title", "All employees:", "these are all our employees", parts);

            // get the xml representation of the report object
            IReportXmlConverter objtoXmlConverter = new ReportXmlConverter(factory);
            var result = objtoXmlConverter.ConvertToReportXml(rpt);

            //create xml document from string
            XDocument xdoc = XDocument.Parse(result);

            // Verification starts
            // verify the root node count
            var reportNode = xdoc.Descendants("ReviewReport");
            Assert.AreEqual(reportNode.Count(), 1);

            // verify the report part (paragraph) count
            var labelElement = reportNode.Descendants("label");
            Assert.IsTrue(labelElement.Count() > employeesCollection.Count * PROPERTY_COUNT);

            // verify the report header count
            Assert.AreEqual(reportNode.Descendants("Header").Count(), 1);

            /* verify the report footer count. There is a 
             * default header (report generated on) and the report header mentioned in the Report object.
             * */
            Assert.AreEqual(reportNode.Descendants("Footer").Count(), 2);
        }
    }
}
