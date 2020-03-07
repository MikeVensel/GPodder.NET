// <copyright file="SubscriptionsTests.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using GPodder.NET.Enumerations;
    using GPodder.NET.Models;
    using Microsoft.Extensions.Configuration;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests for the <see cref="Subscriptions"/> class.
    /// </summary>
    [TestClass]
    public class SubscriptionsTests
    {
        private const string DeviceId = "gPodder.NET-test";
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
                .AddUserSecrets<SubscriptionsTests>();
            var configuration = configBuilder.Build();
            client = new GPodderClient(configuration["GpodderUsername"], configuration["GpodderPassword"]);
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
        /// Tests the <see cref="Subscriptions.GetAllSubscriptions"/> method.
        /// </summary>
        /// <returns><see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task TestGetAllSubscriptions()
        {
            var podcastSubscriptions = await client.Subscriptions.GetAllSubscriptions();
            Assert.IsNotNull(podcastSubscriptions);
        }

        /// <summary>
        /// Tests the <see cref="Subscriptions.GetDeviceSubscriptions(string)"/> method.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task TestGetDeviceSubscriptions()
        {
            var podcastSubscriptions = await client.Subscriptions.GetDeviceSubscriptions(DeviceId);
            Assert.IsNotNull(podcastSubscriptions);
        }

        /// <summary>
        /// Tests the <see cref="Subscriptions.GetSubscriptionChanges(string, long)"/> method.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task TestGetDeviceSubscriptionUpdates()
        {
            var deviceSubUpdates = await client.Subscriptions.GetSubscriptionChanges(DeviceId);
            Assert.IsNotNull(deviceSubUpdates);

            // Ensure that get subscriptions using the timestamp we just received returns no changes as it should.
            deviceSubUpdates = await client.Subscriptions.GetSubscriptionChanges(DeviceId, deviceSubUpdates.Timestamp);
            Assert.IsNotNull(deviceSubUpdates);
            Assert.IsTrue(deviceSubUpdates.Add.Count() == 0);
            Assert.IsTrue(deviceSubUpdates.Remove.Count() == 0);
        }

        /// <summary>
        /// Tests the <see cref="Subscriptions.GetDeviceSubscriptions(string)"/> method.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task TestUploadDeviceSubscriptions()
        {
            var deviceId = "gPodder.NET-test";
            var podcastsBeforeUpload = await client.Subscriptions.GetDeviceSubscriptions(deviceId);

            // todo handle this either by providing a sample file as a resource or just creating an in memory stream here with test data.
            var testFilePath = Path.Combine(
                                            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                                            "Downloads",
                                            "antennapod-feeds.opml");
            using var testFileStream = File.Open(testFilePath, FileMode.Open);
            await client.Subscriptions.UploadSubscriptionsOfDevice(deviceId, SubUploadFormat.OPML, testFileStream);
            var podcastsAfterUpload = await client.Subscriptions.GetDeviceSubscriptions(deviceId);
            Assert.IsNotNull(podcastsAfterUpload);
        }

        /// <summary>
        /// Tests the <see cref="Subscriptions.UploadDeviceSubscriptionChanges(string, SubscriptionChanges)"/>.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task TestUploadDeviceSubscriptionChanges()
        {
            var deviceId = "gPodder.NET-test";
            var testSubscriptionChanges = new SubscriptionChanges
            {
                Add = new List<string>()
                {
                    "https://2bears1cave.libsyn.com/rss",
                },
            };

            var updatedSubscriptions = await client.Subscriptions.UploadDeviceSubscriptionChanges(deviceId, testSubscriptionChanges);
            Assert.IsNotNull(updatedSubscriptions);
        }
    }
}