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
            client = new GPodderClient(configuration["GpodderUsername"], configuration["GPodderPassword"]);
            await client.Authentication.Login();
        }

        /// <summary>
        /// Cleans up after all tests have run.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [ClassCleanup]
        public static async Task CleanUpTests()
        {
            await client.Authentication.Logout();
        }

        /// <summary>
        /// Tests the <see cref="Devices.ListDevices"/> method.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task TestListDevices()
        {
            var deviceCollection = await client.Devices.ListDevices();
            Assert.IsNotNull(deviceCollection);
            foreach (var device in deviceCollection)
            {
                Assert.IsFalse(string.IsNullOrEmpty(device.Id));
            }
        }

        /// <summary>
        /// Tests the <see cref="Devices.UpdateDeviceData(Device)"/> method
        /// when creating a new device.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task TestCreateDevice()
        {
            var deviceCollection = await client.Devices.ListDevices();
            var deviceCount = deviceCollection.Count();
            var newDevice = new Device()
            {
                Id = "gPodder.NET-test",
                Caption = "Test New Laptop",
                Type = "laptop",
            };

            await client.Devices.UpdateDeviceData(newDevice);
            deviceCollection = await client.Devices.ListDevices();
            Assert.IsTrue(deviceCollection.Any(device =>
            {
                return device.Id == newDevice.Id
                       && device.Caption == newDevice.Caption
                       && device.Type == newDevice.Type;
            }));
        }

        /// <summary>
        /// Tests the <see cref="Devices.UpdateDeviceData(Device)"/> method.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task TestUpdateDeviceData()
        {
            var deviceCollection = await client.Devices.ListDevices();
            Assert.IsNotNull(deviceCollection);
            Assert.IsTrue(deviceCollection.Count() > 0);
            var updatedDevice = deviceCollection.First();
            var currentCaption = updatedDevice.Caption;
            updatedDevice.Caption = "Test change caption";
            await client.Devices.UpdateDeviceData(updatedDevice);
            deviceCollection = await client.Devices.ListDevices();
            Assert.IsTrue(deviceCollection.Any(d => d.Caption == updatedDevice.Caption));
            updatedDevice.Caption = currentCaption;
            await client.Devices.UpdateDeviceData(updatedDevice);
        }

        /// <summary>
        /// Tests the <see cref="Devices.GetDeviceUpdates(string, long, bool)"/> method.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task TestGetDeviceUpdates()
        {
            var deviceCollection = await client.Devices.ListDevices();
            var device = deviceCollection.First();
            var lastDateChecked = 0;
            var deviceUpdates = await client.Devices.GetDeviceUpdates(device.Id, lastDateChecked, true);
            Assert.IsNotNull(deviceUpdates);
            Assert.IsTrue(deviceUpdates.LastUpdatedTimestamp > 0);
        }
    }
}
