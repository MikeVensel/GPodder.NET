// <copyright file="SubscriptionsTests.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET.Tests
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests for the <see cref="Subscriptions"/> class.
    /// </summary>
    [TestClass]
    public class SubscriptionsTests
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
        public async Task CleanUpTests()
        {
            await client.Authentication.Logout(username);
        }

        /// <summary>
        /// Tests the <see cref="Subscriptions.GetAllSubscriptions(string)"/> method.
        /// </summary>
        /// <returns> <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task TestGetAllSubscriptions()
        {
            await client.Subscriptions.GetAllSubscriptions(username);
        }
    }
}