namespace IoT.Data.Engine
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class representing an engine's properties.
    /// </summary>
    public class Engine
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the operating temperatures.
        /// </summary>
        public int[] OperatingTemperaturesBetween { get; set; }

        /// <summary>
        /// Gets or sets the maximum temperature.
        /// </summary>
        public int WarningTemperature { get; set; }

        /// <summary>
        /// Gets or sets the seconds between which the engine is running.
        /// </summary>
        public int[] RunningTimesBetween { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the engine is running.
        /// </summary>
        public bool IsRunning => true;//DateTime.Now.Second > RunningTimesBetween[0] && DateTime.Now.Second <= RunningTimesBetween[1];

        /// <summary>
        /// Gets or sets the hours currently on the clock.
        /// </summary>
        public int CurrentClockValue { get; set; }

        /// <summary>
        /// Gets or sets the hours between filter changes.
        /// </summary>
        public int HoursBetweenFilterChanges { get; set; }

        /// <summary>
        /// Gets or sets a value indicating if a filter change should be done soon.
        /// </summary>
        public bool FilterChangeUpcoming => CurrentClockValue > HoursBetweenFilterChanges - 200;

        /// <summary>
        /// Get a list with sample engines for the simulation.
        /// </summary>
        /// <returns></returns>
        public static List<Engine> GetSampleEngines()
        {
            return new List<Engine>
                       {
                           new Engine
                               {
                                   Name = "Main Engine",
                                   OperatingTemperaturesBetween = new []{100, 250},
                                   WarningTemperature = 225,
                                   RunningTimesBetween = new []{0, 40},
                                   CurrentClockValue = 995,
                                   HoursBetweenFilterChanges = 1000
                               },
                           new Engine
                               {
                                   Name = "Propeller Engine",
                                   OperatingTemperaturesBetween = new []{50, 200},
                                   WarningTemperature = 195,
                                   RunningTimesBetween = new []{0, 40},
                                   CurrentClockValue = 200,
                                   HoursBetweenFilterChanges = 500
                               },
                           new Engine
                               {
                                   Name = "Forward Engine",
                                   OperatingTemperaturesBetween = new []{50, 200},
                                   WarningTemperature = 195,
                                   RunningTimesBetween = new []{40, 60},
                                   CurrentClockValue = 440,
                                   HoursBetweenFilterChanges = 500
                               },
                           new Engine
                               {
                                   Name = "Rear Engine",
                                   OperatingTemperaturesBetween = new []{100, 200},
                                   WarningTemperature = 195,
                                   RunningTimesBetween = new []{45, 55},
                                   CurrentClockValue = 180,
                                   HoursBetweenFilterChanges = 2000
                               },
                           new Engine
                               {
                                   Name = "Pump Engine",
                                   OperatingTemperaturesBetween = new []{100, 200},
                                   WarningTemperature = 195,
                                   RunningTimesBetween = new []{45, 55},
                                   CurrentClockValue = 620,
                                   HoursBetweenFilterChanges = 2000
                               }
                       };
        }
    }
}