using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using FileToHtmlTextConverter = TDDMicroExercises.UnicodeFileToHtmlTextConverter.UnicodeFileToHtmlTextConverter;

namespace TDDMicroExercises.Tests.UnicodeFileToHtmlTextConverter
{
    public class UnicodeFileToHtmlTextConverterTests
    {
        [Fact]
        public void UnicodeFileToHtmlTextConverter_Emtpy()
        {
            // Arrange
            var converter = new FileToHtmlTextConverter(@"UnicodeFileToHtmlTextConverter\Data\Empty.txt");
            var expectedResult = "";

            // Act
            var html = converter.ConvertToHtml();

            // Assert
            html.Should().Be(expectedResult);
        }

        [Fact]
        public void UnicodeFileToHtmlTextConverter_MultiLine()
        {
            // Arrange
            var converter = new FileToHtmlTextConverter(@"UnicodeFileToHtmlTextConverter\Data\Multiline.txt");
            var expectedResult = "Lorem ipsum dolor sit amet,<br />" + 
                                  "consectetur adipiscing elit,<br />" +
                                  "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.<br />";

            // Act
            var html = converter.ConvertToHtml();

            // Assert
            html.Should().Be(expectedResult);
        }
    }
}
