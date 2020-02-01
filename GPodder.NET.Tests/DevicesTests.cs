// <copyright file="DevicesTests.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using GPodder.NET.Models;
    using Microsoft.Extensions.Configuration;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for the <see cref="Devices"/> class.
    /// </summary>
    [TestClass]
    public class DevicesTests
    {
        private static string username;
        private static GPodderClient client;

        /// <summary>
        /// Initializes necessary components for tests.
        /// </summary>
        /// <param name="testContext">The <see cref="TestContext"/> for the unit tests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [ClassInitialize]
        public static async Task InitTests(TestContext testContext)
        {
            var configBuilder = new ConfigurationBuilder()
                .AddUserSecrets<DevicesTests>();
            var configuration = configBuilder.Build();
            username = configuration["GpodderUsername"];
            client = new GPodderClient();
            await client.Authentication.Login(username, configuration["GPodderPassword"]);
        }

        /// <summary>
        /// Cleans up after all tests have run.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [ClassCleanup]
        public static async Task CleanUpTests()
        {
            await client.Authentication.Logout(username);
        }

        /// <summary>
        /// Tests the <see cref="Devices.ListDevices(string)"/> method.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task TestListDevices()
        {
            var deviceCollection = await client.Devices.ListDevices(username);
            Assert.IsNotNull(deviceCollection);
            foreach (var device in deviceCollection)
            {
                Assert.IsFalse(string.IsNullOrEmpty(device.Id));
            }
        }

        /// <summary>
        /// Tests the <see cref="Devices.UpdateDeviceData(string, Device)"/> method
        /// when creating a new device.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task TestCreateDevice()
        {
            var deviceCollection = await client.Devices.ListDevices(username);
            var deviceCount = deviceCollection.Count();
            var newDevice = new Device()
            {
                Id = Guid.NewGuid().ToString(),
                Caption = "Test New Laptop",
                Type = "laptop",
            };

            await client.Devices.UpdateDeviceData(username, newDevice);
            deviceCollection = await client.Devices.ListDevices(username);
            Assert.IsTrue(deviceCollection.Count() == (deviceCount + 1));
        }

        /// <summary>
        /// Tests the <see cref="Devices.UpdateDeviceData(string, Models.Device)"/> method.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task TestUpdateDeviceData()
        {
            var deviceCollection = await client.Devices.ListDevices(username);
            Assert.IsNotNull(deviceCollection);
            Assert.IsTrue(deviceCollection.Count() > 0);
            var updatedDevice = deviceCollection.First();
            var currentCaption = updatedDevice.Caption;
            updatedDevice.Caption = "Test change caption";
            await client.Devices.UpdateDeviceData(username, updatedDevice);
            deviceCollection = await client.Devices.ListDevices(username);
            Assert.IsTrue(deviceCollection.Any(d => d.Caption == updatedDevice.Caption));
            updatedDevice.Caption = currentCaption;
            await client.Devices.UpdateDeviceData(username, updatedDevice);
        }

        /// <summary>
        /// Tests the <see cref="Devices.GetDeviceUpdates(string, string, DateTime, bool)"/> method.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task TestGetDeviceUpdates()
        {
            var deviceCollection = await client.Devices.ListDevices(username);

            // todo setup a device for testing
            var device = deviceCollection.First(d => d.Caption.Contains("PLACEHOLDER"));
            var lastDateChecked = new DateTime(2020, 1, 31);
            var deviceUpdates = await client.Devices.GetDeviceUpdates(username, device.Id, lastDateChecked, true);
        }
    }
}
