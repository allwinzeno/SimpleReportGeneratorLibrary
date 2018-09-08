namespace ReportGeneratorUtils
{
    /// <summary>
    /// Report content type
    /// </summary>
    public enum ContentType
    {
        /// <summary>
        /// The content will be displayed as a hmtl table. 
        /// Each object in the collection will be represented as a row.
        /// The text in the ReportDisplay attribute will be displayed in the header of the table.
        /// </summary>
        Table = 0,
        /// <summary>
        /// The content (each object) will be displayed as a HTML label of format.
        /// Text in the ReportDisplay attribute: Value of the property
        /// </summary>
        Label = 2,
        /// <summary>
        /// The content (each object) will be displayed as a HTML paragraph of format.
        /// Text in the ReportDisplay attribute: 
        /// <p>Value of the property</p>
        /// </summary>
        Paragraph = 3
    }

    public enum ReportType
    {
        Html = 0
    }
}
