using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Moq;
using TDDMicroExercises.UnicodeFileToHtmlTextConverter;
using System.IO;

namespace TDDMicroExercises.Tests.UnicodeFileToHtmlTextConverter
{
    public class HtmlConverterTests
    {
        [Theory]
        [InlineData("a < b", "a &lt; b")]
        [InlineData("a > b", "a &gt; b")]
        [InlineData("a & b", "a &amp; b")]
        public void HtmlConverter_EncodeText(string text, string expectedResult)
        {
            // Arrange
            var converter = new HtmlConverter();

            // Act
            var html = converter.EncodeText(text);

            // Assert
            html.Should().Be(expectedResult);
        }

        [Fact]
        public void HtmlConverter_EncodeLine()
        {
            // Arrange
            var converter = new HtmlConverter();

            // Act
            var html = converter.EncodeLine("line");

            // Assert
            html.Should().Be("line" + HtmlConst.LineBreak);
        }
    }
}
