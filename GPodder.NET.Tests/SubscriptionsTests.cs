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
                .AddUserSecrets<SubscriptionsTests>();
            var configuration = configBuilder.Build();
            username = configuration["GpodderUsername"];
            client = new GPodderClient();
            await client.Authentication.Login(username, configuration["GpodderPassword"]);
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
        /// Tests the <see cref="Subscriptions.GetAllSubscriptions(string)"/> method.
        /// </summary>
        /// <returns><see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task TestGetAllSubscriptions()
        {
            var podcastSubscriptions = await client.Subscriptions.GetAllSubscriptions(username);
            Assert.IsNotNull(podcastSubscriptions);
        }

        /// <summary>
        /// Tests the <see cref="Subscriptions.GetDeviceSubscriptions(string, string)"/> method.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task TestGetDeviceSubscriptions()
        {
            var podcastSubscriptions = await client.Subscriptions.GetDeviceSubscriptions(username, DeviceId);
            Assert.IsNotNull(podcastSubscriptions);
        }

        /// <summary>
        /// Tests the <see cref="Subscriptions.GetSubscriptionChanges(string, string, int)"/> method.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task TestGetDeviceSubscriptionUpdates()
        {
            var deviceSubUpdates = await client.Subscriptions.GetSubscriptionChanges(username, DeviceId);
            Assert.IsNotNull(deviceSubUpdates);

            // Ensure that get subscriptions using the timestamp we just received returns no changes as it should.
            deviceSubUpdates = await client.Subscriptions.GetSubscriptionChanges(username, DeviceId, deviceSubUpdates.Timestamp);
            Assert.IsNotNull(deviceSubUpdates);
            Assert.IsTrue(deviceSubUpdates.Add.Count() == 0);
            Assert.IsTrue(deviceSubUpdates.Remove.Count() == 0);
        }

        /// <summary>
        /// Tests the <see cref="Subscriptions.GetDeviceSubscriptions(string, string)"/> method.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task TestUploadDeviceSubscriptions()
        {
            var deviceId = "gPodder.NET-test";
            var podcastsBeforeUpload = await client.Subscriptions.GetDeviceSubscriptions(username, deviceId);

            // todo handle this either by providing a sample file as a resource or just creating an in memory stream here with test data.
            var testFilePath = Path.Combine(
                                            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                                            "Downloads",
                                            "antennapod-feeds.opml");
            using var testFileStream = File.Open(testFilePath, FileMode.Open);
            await client.Subscriptions.UploadSubscriptionsOfDevice(username, deviceId, SubUploadFormat.OPML, testFileStream);
            var podcastsAfterUpload = await client.Subscriptions.GetDeviceSubscriptions(username, deviceId);
            Assert.IsNotNull(podcastsAfterUpload);
            Assert.IsTrue(podcastsAfterUpload.Count() >= podcastsBeforeUpload.Count());
        }

        /// <summary>
        /// Tests the <see cref="Subscriptions.UploadDeviceSubscriptionChanges(string, string, Models.SubscriptionChanges)"/>.
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

            var updatedSubscriptions = await client.Subscriptions.UploadDeviceSubscriptionChanges(username, deviceId, testSubscriptionChanges);
            Assert.IsNotNull(updatedSubscriptions);
        }
    }
}