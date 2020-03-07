// <copyright file="EpisodeActionTests.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET.Tests
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests for the <see cref="EpisodeActions"/> class.
    /// </summary>
    [TestClass]
    public class EpisodeActionTests
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
        /// Tests the <see cref="EpisodeActions.GetEpisodeActions(string, string, long, bool)"/> method.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task TestGetEpisodeActionsAsync()
        {
            var episodeActionsResponse = await client.EpisodeActions.GetEpisodeActions();
            Assert.IsNotNull(episodeActionsResponse);
            Assert.IsNotNull(episodeActionsResponse.ActionsList);
            Assert.IsInstanceOfType(episodeActionsResponse.Timestamp, typeof(long));
        }

        /// <summary>
        /// Tests the <see cref="EpisodeActions.UploadEpisodeActions(System.Collections.Generic.IEnumerable{Models.EpisodeAction})"/> method.
        /// </summary>
        [TestMethod]
        public void TestUploadEpisodeActions()
        {
        }
    }
}
