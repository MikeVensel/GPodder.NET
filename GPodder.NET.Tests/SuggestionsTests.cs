// <copyright file="SuggestionsTests.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET.Tests
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for the <see cref="Suggestions"/> class.
    /// </summary>
    [TestClass]
    public class SuggestionsTests
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
                .AddUserSecrets<AuthenticationTests>();
            var configuration = configBuilder.Build();
            client = new GPodderClient(configuration["GPodderUsername"], configuration["GpodderPassword"]);
            await client.Authentication.Login();
        }

        /// <summary>
        /// Cleans up after all tests have run.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [ClassCleanup]
        public static async Task CleanUpTests()
        {
            await client.Authentication.Logout();
        }

        /// <summary>
        /// Tests the <see cref="Suggestions.RetrieveSuggestedPodcasts(int)"/> method.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task TestRetrievePodcastSuggestions()
        {
            var podcastCollection = await client.Suggestions.RetrieveSuggestedPodcasts(10);
            Assert.IsNotNull(podcastCollection);
        }
    }
}
