using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using FileToHtmlTextConverter = TDDMicroExercises.UnicodeFileToHtmlTextConverter.UnicodeFileToHtmlTextConverter;
using Moq;
using TDDMicroExercises.UnicodeFileToHtmlTextConverter;
using System.IO;

namespace TDDMicroExercises.Tests.UnicodeFileToHtmlTextConverter
{
    public class UnicodeFileToHtmlTextConverterTests
    {
        [Fact]
        public void UnicodeFileToHtmlTextConverter_ConvertToHtml_Emtpy()
        {
            // Arrange
            var converter = new FileToHtmlTextConverter(@"UnicodeFileToHtmlTextConverter\Data\Empty.txt");
            var expectedResult = "";

            // Act
            var html = converter.ConvertToHtml();

            // Assert
            html.Should().Be(expectedResult, "Empty file should return empty html");
        }

        [Fact]
        public void UnicodeFileToHtmlTextConverter_ConvertToHtml_MultiLine()
        {
            // Arrange
            var converter = new FileToHtmlTextConverter(@"UnicodeFileToHtmlTextConverter\Data\Multiline.txt");
            var expectedResult = "Lorem &amp; ipsum dolor sit amet,<br />" + 
                                  "consectetur &amp; adipiscing elit,<br />" +
                                  "sed &amp; do eiusmod tempor incididunt ut labore et dolore magna aliqua.<br />";

            // Act
            var html = converter.ConvertToHtml();

            // Assert
            html.Should().Be(expectedResult, "Every line should be encoded and line endings replaced with an html line break");
        }
    }
}
