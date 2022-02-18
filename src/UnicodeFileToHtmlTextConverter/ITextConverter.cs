namespace TDDMicroExercises.UnicodeFileToHtmlTextConverter
{
    public interface ITextConverter
    {
        string EncodeText(string text);
        string EncodeLine(string line);
    }


}
