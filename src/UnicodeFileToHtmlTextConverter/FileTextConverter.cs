using System.IO;
using System.Text;

namespace TDDMicroExercises.UnicodeFileToHtmlTextConverter
{
    public class FileTextConverter : StreamTextConverter
    {
        public FileTextConverter(ITextConverter textConverter) : base(textConverter)
        {
        }

        public string ConvertFileAndReadToEnd(string fullFilenameWithPath, Encoding encoding)
        {
            using (var fs = File.Open(fullFilenameWithPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return ConvertStreamAndReadToEnd(fs, encoding);
            }
        }
    }
}
