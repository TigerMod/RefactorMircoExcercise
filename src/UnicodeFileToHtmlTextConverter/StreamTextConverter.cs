using System.IO;
using System.Text;

namespace TDDMicroExercises.UnicodeFileToHtmlTextConverter
{
    public class StreamTextConverter
    {
        protected ITextConverter _textConverter;

        public StreamTextConverter(ITextConverter textConverter)
        {
            _textConverter = textConverter;
        }

        public void ConvertStream(Stream input, Stream output, Encoding encoding)
        {
            using (var sr = new StreamReader(input, encoding))
            {
                using (var sw = new StreamWriter(output, encoding: encoding, bufferSize: 4096, leaveOpen: true))
                {
                    while (sr.Peek() != -1)
                    {
                        var line = sr.ReadLine();
                        var encodedLine = _textConverter.EncodeLine(line);
                        sw.Write(encodedLine);
                    }
                }
            }
        }

        public string ConvertStreamAndReadToEnd(Stream inputStream, Encoding encoding)
        {
            using (var ms = new MemoryStream())
            {
                ConvertStream(input: inputStream, output: ms, encoding: encoding);
                ms.Position = 0;
                using (var sr = new StreamReader(ms))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }


}
