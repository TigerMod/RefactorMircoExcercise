using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDDMicroExercises.UnicodeFileToHtmlTextConverter;
using Xunit;

namespace TDDMicroExercises.Tests.UnicodeFileToHtmlTextConverter
{
    public class StreamTextConverterTests
    {
        [Fact]
        public void StreamTextConverter_ConvertStreamAndReadToEnd_CustomTextConverter()
        {
            // Arrange
            var textConverterMock = new Mock<ITextConverter>();
            textConverterMock.Setup(c => c.EncodeLine(It.IsAny<string>()))
                .Returns((string s) => { return "line|"; }); // each line is encoded as 'line|'

            var converter = new StreamTextConverter(textConverterMock.Object);

            var textToConvert = "lorem\ripsum\ndolor et";
            var expectedResult = "line|line|line|";

            // Act
            var bytes = Encoding.Unicode.GetBytes(textToConvert);
            string result;
            using (var ms = new MemoryStream(bytes))
            {
                result = converter.ConvertStreamAndReadToEnd(ms, Encoding.Unicode);
            }

            // Assert
            result.Should().Be(expectedResult);
            textConverterMock.Verify(c => c.EncodeLine("lorem"), Times.Once);
            textConverterMock.Verify(c => c.EncodeLine("ipsum"), Times.Once);
            textConverterMock.Verify(c => c.EncodeLine("dolor et"), Times.Once);
            textConverterMock.Verify(c => c.EncodeText(It.IsAny<string>()), Times.Never);
        }
    }
}
