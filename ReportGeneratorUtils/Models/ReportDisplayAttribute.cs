using System;

namespace ReportGeneratorUtils
{
    /// <summary>
    /// Attribute used for mentioning the display text of the particulat field. 
    /// If it is used for a class the display text will be used 
    /// for title/subtitles in the report.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public sealed class ReportDisplayAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportDisplayAttribute"/> class.
        /// </summary>
        /// <param name="displayText">The display text.</param>
        public ReportDisplayAttribute(string displayText)
        {
            this.DisplayText = displayText;
        }

        /// <summary>
        /// Gets the display text.
        /// </summary>
        /// <value>
        /// The display text.
        /// </value>
        public string DisplayText { get; }
    }
}
