namespace ReportGeneratorUtils.Utils
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
}
