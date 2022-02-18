using System.Web;

namespace TDDMicroExercises.UnicodeFileToHtmlTextConverter
{
    public class HtmlConverter : ITextConverter
    {
        public string EncodeText(string text)
        {
            return HttpUtility.HtmlEncode(text);
        }

        public string EncodeLine(string line)
        {
            return $"{EncodeText(line)}{HtmlConst.LineBreak}";
        }
    }


}
