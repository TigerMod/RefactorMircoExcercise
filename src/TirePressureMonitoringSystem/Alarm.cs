namespace TDDMicroExercises.TirePressureMonitoringSystem
{
    public class Alarm
    {
        public const double DefaultLowPressureThreshold = 17;
        public const double DefaultHighPressureThreshold = 21;

        private readonly ISensor _sensor;
        private readonly double _lowPressureThreshold;
        private readonly double _highPressureThreshold;
        private bool _alarmOn = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Alarm"/> class
        /// </summary>
        /// <param name="sensor">A sensor object that will be used by this alarm for fetching sensor values.</param>
        public Alarm(ISensor sensor) : this(sensor, DefaultLowPressureThreshold, DefaultHighPressureThreshold)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Alarm"/> class
        /// </summary>
        /// <param name="sensor">A sensor object that will be used by this alarm for fetching sensor values.</param>
        /// <param name="lowPressureThreshold">Any value retuned by the sensor that is lower than this should trigger an alarm</param>
        /// <param name="highPressureThreshold">Any value returned by the sensor that is higher than this should trigger an alarm</param>
        public Alarm(ISensor sensor, double lowPressureThreshold, double highPressureThreshold)
        {
            _sensor = sensor;
            _lowPressureThreshold = lowPressureThreshold;
            _highPressureThreshold = highPressureThreshold;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Alarm"/> class using an new instance of <see cref="Sensor"/>
        /// </summary>
        public Alarm(): this(new Sensor()) { }

        public void Check()
        {
            double psiPressureValue = _sensor.PopNextPressurePsiValue();

            if (psiPressureValue < _lowPressureThreshold || _highPressureThreshold < psiPressureValue)
            {
                _alarmOn = true;
            }
        }

        public bool AlarmOn
        {
            get { return _alarmOn; }
        }

    }
}
