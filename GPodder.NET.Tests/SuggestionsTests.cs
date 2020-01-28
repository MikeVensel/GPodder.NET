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
        /// <summary>
        /// Initializes a new instance of the <see cref="SuggestionsTests"/> class.
        /// </summary>
        public SuggestionsTests()
        {
            var configBuilder = new ConfigurationBuilder()
                .AddUserSecrets<AuthenticationTests>();
            this.Configuration = configBuilder.Build();
        }

        private IConfiguration Configuration { get; set; }

        /// <summary>
        /// Tests the <see cref="Suggestions.RetrieveSuggestedPodcasts(int)"/> method.
        /// </summary>
        [TestMethod]
        public void TestRetrievePodcastSuggestions()
        {
            var client = new GPodderClient();
            Task.Run(async () =>
            {
                await client.Authentication.Login(this.Configuration["GpodderUsername"], this.Configuration["GpodderPassword"]);
                var podcastCollection = await client.Suggestions.RetrieveSuggestedPodcasts(10);
                Assert.IsNotNull(podcastCollection);
            }).GetAwaiter().GetResult();
        }
    }
}
