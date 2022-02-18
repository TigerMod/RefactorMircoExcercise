using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDDMicroExercises.TirePressureMonitoringSystem;
using Xunit;
using FluentAssertions;
using Moq;

namespace TDDMicroExercises.Tests.TirePressureMonitoringSystem
{
    public class AlarmTests
    {
        [Fact]
        public void Alarm_AlarmOn_Is_False_By_Default()
        {
            // Arrange
            var alarm = new Alarm();

            // Act
            var alarmOn = alarm.AlarmOn;

            // Assert
            Assert.False(alarmOn, "Alarm.AlarmOn should be false by default");
        }

        [Theory]
        [InlineData(Alarm.DefaultLowPressureThreshold - 0.1, true)] // below low pressure threshold
        [InlineData(Alarm.DefaultLowPressureThreshold, false)] // at low pressure threshold
        [InlineData(Alarm.DefaultLowPressureThreshold + 1, false)] // between low and high pressure thresholds
        [InlineData(Alarm.DefaultHighPressureThreshold, false)] // at high pressure threshold
        [InlineData(Alarm.DefaultHighPressureThreshold + 0.1, true)] // above high pressure threshold
        public void Alarm_Check_Sets_Alarm_Correctly_At_Default_Thresholds(double sensorValue, bool expectedAlarmOnValue)
        {
            // Arrange
            var sensorMock = new Mock<ISensor>();
            sensorMock.Setup(s => s.PopNextPressurePsiValue())
                .Returns(sensorValue);
            var alarm = new Alarm(sensorMock.Object);

            // Act
            alarm.Check();
            var alarmOn = alarm.AlarmOn;

            // Assert
            alarmOn.Should().Be(expectedAlarmOnValue);
        }

        [Theory]
        [InlineData(1.0, 3.0, 1.0 - 0.1, true)] // below low pressure threshold
        [InlineData(1.0, 3.0, 1.0, false)] // at low pressure threshold
        [InlineData(1.0, 3.0, 1.0 + 1, false)] // between low and high pressure thresholds
        [InlineData(1.0, 3.0, 3.0, false)] // at high pressure threshold
        [InlineData(1.0, 3.0, 3.0 + 0.1, true)] // above high pressure threshold
        public void Alarm_Check_Sets_Alarm_Correctly_At_Custom_Thresholds(double lowPressureThreshold, double highPressureThreshold, double sensorValue, bool expectedAlarmOnValue)
        {
            // Arrange
            var sensorMock = new Mock<ISensor>();
            sensorMock.Setup(s => s.PopNextPressurePsiValue())
                .Returns(sensorValue);
            var alarm = new Alarm(sensorMock.Object, lowPressureThreshold, highPressureThreshold);

            // Act
            alarm.Check();
            var alarmOn = alarm.AlarmOn;

            // Assert
            alarmOn.Should().Be(expectedAlarmOnValue);
        }

        [Fact]
        public void Alarm_AlarmOn_Remains_True()
        {
            // Arrange
            var sensorMock = new Mock<ISensor>();
            sensorMock.SetupSequence(s => s.PopNextPressurePsiValue())
                .Returns(Alarm.DefaultLowPressureThreshold - 1) // first it returns too low value
                .Returns(Alarm.DefaultLowPressureThreshold); // 2nd it returns value within the accepted range
            var alarm = new Alarm(sensorMock.Object);

            // Act
            alarm.Check();
            var alarmOn1 = alarm.AlarmOn;

            alarm.Check();
            var alarmOn2 = alarm.AlarmOn;

            // Assert
            Assert.True(alarmOn1, "alarmOn should be true after the first reading");
            Assert.True(alarmOn2, "alarmon should remain true even if the second reading was within the normal range");
        }

        [Fact]
        public void Alarm_AlarmOn_Returns_True_Twice()
        {
            // Arrange
            var sensorMock = new Mock<ISensor>();
            sensorMock.Setup(s => s.PopNextPressurePsiValue())
                .Returns(Alarm.DefaultLowPressureThreshold - 1); // it always returns too low value
            var alarm = new Alarm(sensorMock.Object);

            // Act
            alarm.Check();
            var alarmOn1 = alarm.AlarmOn;
            var alarmOn2 = alarm.AlarmOn;

            // Assert
            Assert.True(alarmOn1, "alarmOn should be true after the first reading");
            Assert.True(alarmOn2, "alarmon should be true after the second check if no new sensor check happened");
        }

        [Fact]
        public void Alarm_Check_Calls_Sensor_Once()
        {
            // Arrange
            var sensorMock = new Mock<Sensor>() { CallBase = true }; // keep the original implementation
            var alarm = new Alarm(sensorMock.Object);

            // Act & Assert
            sensorMock.Verify(s => s.PopNextPressurePsiValue(), Times.Never, "PopNextPressurePsiValue should NOT be called from constructor of Alarm");
            alarm.Check();
            sensorMock.Verify(s => s.PopNextPressurePsiValue(), Times.Once, "PopNextPressurePsiValue should be called once by method Check of Alarm");
        }

        [Fact]
        public void Alarm_AlarmOn_Does_Not_Call_Sensor()
        {
            // Arrange
            var sensorMock = new Mock<Sensor>() { CallBase = true }; // keep the original implementation
            var alarm = new Alarm(sensorMock.Object);

            // Act & Assert
            var value = alarm.AlarmOn;
            sensorMock.Verify(s => s.PopNextPressurePsiValue(), Times.Never, "PopNextPressurePsiValue should NOT be called by method Property AlarmOn of Alarm");
        }
    }
}
