using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using IoT.Data.Engine;

namespace SimulatedDevice
{
    class Program
    {
        static DeviceClient deviceClient;
        static string iotHubUri = "IoT.azure-devices.net";

        static string deviceKey = "enterdevicekey";
        static void Main(string[] args)
        {
            Console.WriteLine("Simulated device\n");
            deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey("PoCIoTDevice", deviceKey), TransportType.Mqtt);

            Send();
            Console.ReadLine();
        }

        internal static async void Send()
        {
            // Get sample engines
            var engines = Engine.GetSampleEngines();

            // Run continuously
            while (true)
            {
                // Loop through all ships
                foreach (var ship in GetShips())
                {
                    // Loop through all engines
                    foreach (var engine in engines)
                    {
                        // Set values for when engine is not running
                        var temperature = 0;
                        var oilPressure = 0;
                        var fuelConsumption = 0;
                        var rpm = 0;
                        var clockValue = engine.CurrentClockValue;
                        var tempAlarm = false;
                        var pressureAlarm = false;
                        var filterChangeComing = engine.FilterChangeUpcoming;
                        var currentTime = DateTime.Now;

                        // Check if engine is running
                        if (engine.IsRunning)
                        {
                            // Set values for when engine is running
                            // For demo purposes, we will make temperature leading for other values
                            temperature = new Random().Next(engine.OperatingTemperaturesBetween[0], engine.OperatingTemperaturesBetween[1]);
                            oilPressure = temperature * 30;
                            fuelConsumption = temperature / 10;
                            rpm = temperature * 10;
                            clockValue = engine.CurrentClockValue++;
                            tempAlarm = temperature > engine.WarningTemperature;
                            pressureAlarm = oilPressure > engine.WarningTemperature * 28;
                        }

                        // Send new EngineReadings object to the IoT Hub
                        await
                            Send(
                                new EngineReadings
                                {
                                    Ship = ship,
                                    Engine = engine.Name,
                                    Temperature = temperature,
                                    FuelConsumption = fuelConsumption,
                                    OilPressure = oilPressure,
                                    RPM = rpm,
                                    TemperatureAlarmActivated = tempAlarm,
                                    OilPressureAlarmActivated = pressureAlarm,
                                    ClockValue = clockValue,
                                    FilterChangeComing = filterChangeComing,
                                    Time = currentTime
                                });

                        // Console info
                        Console.WriteLine($"--- {currentTime} --- Ship: {ship} Engine: {engine.Name} Temperature: {temperature} Clock: {clockValue}");

                        // Do not constantly warn for upcoming filter changes
                        if (filterChangeComing)
                        {
                            engine.HoursBetweenFilterChanges += 500;
                        }
                    }
                }

                // Wait
                await Task.Delay(9000);
            }
           
        }
        private const bool SIMULATE_MULTIPLE_SHIPS = true;

        private static IEnumerable<string> GetShips()
        {
            return SIMULATE_MULTIPLE_SHIPS ? new[] { "Stolt Achievement", "Stolt Capability", "Stolt Concept", "Stolt Confidence", "Stolt Courage" } : new[] { "Hydra"};
        }
        internal static async Task Send(EngineReadings engineReadings)
        {
            await deviceClient.SendEventAsync(new Message(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(engineReadings))));
        }
        private static async void SendDeviceToCloudMessagesAsync()
        {
            double minTemperature = 20;
            double minHumidity = 60;
            Random rand = new Random();

            while (true)
            {
                double currentTemperature = minTemperature + rand.NextDouble() * 15;
                double currentHumidity = minHumidity + rand.NextDouble() * 20;
                string ship = "Stolten";
                var telemetryDataPoint = new
                {
                    deviceId = "PoCIoTDevice",
                    ship = ship,
                    temperature = currentTemperature,
                    humidity = currentHumidity
                };
                var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));
                message.Properties.Add("temperatureAlert", (currentTemperature > 30) ? "true" : "false");

                await deviceClient.SendEventAsync(message);
                Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);

                await Task.Delay(1000);
            }
        }
    }
}
