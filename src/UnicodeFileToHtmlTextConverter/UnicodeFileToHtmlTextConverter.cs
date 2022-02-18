using System;
using System.IO;
using System.Text;

namespace TDDMicroExercises.UnicodeFileToHtmlTextConverter
{
    public sealed class UnicodeFileToHtmlTextConverter : FileTextConverter
    {
        private readonly string _fullFilenameWithPath;

        // For backward compatibility keep the old interface and default to html conversion
        public UnicodeFileToHtmlTextConverter(string fullFilenameWithPath) : base(new HtmlConverter())
        {
            _fullFilenameWithPath = fullFilenameWithPath;
        }

        public string ConvertToHtml()
        {
            return ConvertFileAndReadToEnd(_fullFilenameWithPath);
        }

        public string ConvertFileAndReadToEnd(string fullFilenameWithPath)
        {
            return ConvertFileAndReadToEnd(fullFilenameWithPath, Encoding.Unicode);
        }

        public string ConvertStreamAndReadToEnd(Stream inputStream)
        {
            return ConvertStreamAndReadToEnd(inputStream, Encoding.Unicode);
        }
    }
}
