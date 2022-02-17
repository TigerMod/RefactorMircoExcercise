using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDDMicroExercises.TirePressureMonitoringSystem;
using Xunit;
using FluentAssertions;

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
        [InlineData(16, true)] // below low pressure threshold
        [InlineData(17, false)] // at low pressure threshold
        [InlineData(18, false)] // between low and high pressure thresholds
        [InlineData(21, false)] // at high pressure threshold
        [InlineData(22, true)] // above high pressure threshold
        public void Alarm_Check_Sets_Alarm_Correctly_At_Default_Thresholds(int sensorValue, bool expectedAlarmOnValue)
        {
            // Arrange
            var alarm = new Alarm();

            // Act
            // TODO: sensor should return sensorValue on next read
            alarm.Check();
            var alarmOn = alarm.AlarmOn;

            // Assert
            alarmOn.Should().Be(expectedAlarmOnValue, "alarmOn value mismatch");
        }

        [Fact]
        public void Alarm_AlarmOn_Returns_True_Then_False()
        {
            // Arrange
            var alarm = new Alarm();

            // Act
            // TODO: sensor should return out of threshold value for next
            alarm.Check();
            var alarmOn1 = alarm.AlarmOn;

            // TODO: sensor should return a value within threshold for next
            alarm.Check();
            var alarmOn2 = alarm.AlarmOn;

            // Assert
            Assert.True(alarmOn1, "alarmOn should be true after the first reading");
            Assert.False(alarmOn2, "alarmon should be false after the second reading");
        }

        [Fact]
        public void Alarm_AlarmOn_Returns_True_Twice()
        {
            // Arrange
            var alarm = new Alarm();

            // Act
            // TODO: sensor should return out of threshold value for next
            alarm.Check();
            var alarmOn1 = alarm.AlarmOn;
            var alarmOn2 = alarm.AlarmOn;

            // Assert
            Assert.True(alarmOn1, "alarmOn should be true after the first reading");
            Assert.True(alarmOn2, "alarmon should be true after the second check if no new sensor check happened");
        }
    }
}
