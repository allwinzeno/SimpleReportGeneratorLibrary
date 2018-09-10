namespace ReportGeneratorUtils.Utils
{
    using System;
    using System.IO;
    using System.Text;

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

                var directory = Directory.CreateDirectory(Path.GetDirectoryName(FileURL));
                if (!Directory.Exists(directory.FullName))
                {
                    Directory.CreateDirectory(directory.FullName);
                }

                File.WriteAllBytes(FileURL, bytesReportContent);
            }

        }

        private byte[] GetBytes(string str)
        {
            return Encoding.Unicode.GetBytes(str);
        }
    }
}
