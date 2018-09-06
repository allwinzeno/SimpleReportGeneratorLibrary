using System;
using System.IO;
using System.Text;

namespace ReportGenTestApp
{
    /// <summary>
    /// Report saver
    /// </summary>
    public interface IReportSaver
    {
        /// <summary>
        /// Saves the report.
        /// </summary>
        /// <param name="OverwriteIfFileExists">if set to <c>true</c> [overwrite if file exists].</param>
        void SaveReport(string fileName, string ReportContent, bool OverwriteIfFileExists);
    }

    /// <summary>
    /// Class has functions to save the report into a file
    /// </summary>
    /// <seealso cref="ReportGeneratorUtils.IReportSaver" />
    public class ReportFileSaver : IReportSaver
    {
        public void SaveReport(string FileURL, string ReportContent, bool OverwriteIfFileExists)
        {
            var bytesReportContent = GetBytes(ReportContent);
            if (File.Exists(FileURL))
            {
                if (OverwriteIfFileExists)
                {
                    File.WriteAllBytes(FileURL, bytesReportContent);
                }
                else
                    throw new Exception("The report file already exists. The report file was not saved.");
            }
            else
            {
                File.WriteAllBytes(FileURL, bytesReportContent);
            }

        }

        private byte[] GetBytes(string str)
        {
            return Encoding.Unicode.GetBytes(str);
        }
    }
}
