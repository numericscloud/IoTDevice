namespace IoT.Data.Engine
{
    using System;

    /// <summary>
    /// Class representing readings from a ship's engine.
    /// </summary>
    public struct EngineReadings
    {
        /// <summary>
        /// Gets or sets the name of the ship.
        /// </summary>
        public string Ship { get; set; }

        /// <summary>
        /// Gets or sets the name of the engine.
        /// </summary>
        public string Engine { get; set; }

        /// <summary>
        /// Gets or sets the temperature.
        /// </summary>
        public int Temperature { get; set; }

        /// <summary>
        /// Gets or sets the oil pressure.
        /// </summary>
        public int OilPressure { get; set; }

        /// <summary>
        /// Gets or sets the fuel consumption.
        /// </summary>
        public int FuelConsumption { get; set; }

        /// <summary>
        /// Gets or sets the RPM.
        /// </summary>
        public int RPM { get; set; }

        /// <summary>
        /// Gets or sets the hours on the clock.
        /// </summary>
        public int ClockValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether temperature alarm is currently activated.
        /// </summary>
        public bool TemperatureAlarmActivated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether oil pressure alarm is currently activated.
        /// </summary>
        public bool OilPressureAlarmActivated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a filter change is coming up.
        /// </summary>
        public bool FilterChangeComing { get; set; }

        /// <summary>
        /// Gets or sets the time the reading was created.
        /// </summary>
        public DateTime Time { get; set; }
    }
}
