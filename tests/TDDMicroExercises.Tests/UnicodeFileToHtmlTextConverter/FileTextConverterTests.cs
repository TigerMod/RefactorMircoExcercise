using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDDMicroExercises.UnicodeFileToHtmlTextConverter;
using Xunit;

namespace TDDMicroExercises.Tests.UnicodeFileToHtmlTextConverter
{
    public class FileTextConverterTests
    {
        [Fact]
        public void FileTextConverter_ConvertFileAndReadToEnd_Emtpy()
        {
            // Arrange
            var converter = new FileTextConverter(new HtmlConverter());
            var expectedResult = "";

            // Act
            var html = converter.ConvertFileAndReadToEnd(@"UnicodeFileToHtmlTextConverter\Data\Empty.txt", Encoding.Unicode);

            // Assert
            html.Should().Be(expectedResult, "Empty file should return empty html");
        }

        [Fact]
        public void FileTextConverter_ConvertFileAndReadToEnd_MultiLine()
        {
            // Arrange
            var converter = new FileTextConverter(new HtmlConverter());
            var expectedResult = "Lorem &amp; ipsum dolor sit amet,<br />" +
                                  "consectetur &amp; adipiscing elit,<br />" +
                                  "sed &amp; do eiusmod tempor incididunt ut labore et dolore magna aliqua.<br />";

            // Act
            var html = converter.ConvertFileAndReadToEnd(@"UnicodeFileToHtmlTextConverter\Data\Multiline.txt", Encoding.Unicode);

            // Assert
            html.Should().Be(expectedResult, "Every line should be encoded and line endings replaced with an html line break");
        }
    }
}
