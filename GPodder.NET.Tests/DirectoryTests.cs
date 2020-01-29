// <copyright file="DirectoryTests.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for the <see cref="Directory"/> class.
    /// </summary>
    [TestClass]
    public class DirectoryTests
    {
        private static GPodderClient client;

        /// <summary>
        /// Initializes necessary components for tests.
        /// </summary>
        /// <param name="testContext">The <see cref="TestContext"/> for the unit tests.</param>
        [ClassInitialize]
        public static void InitTests(TestContext testContext)
        {
            client = new GPodderClient();
        }

        /// <summary>
        /// Tests the <see cref="Directory.GetTags(int)"/> with the default count.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task TestGetTags()
        {
            var tagsCollection = await client.Directory.GetTags();
            Assert.IsNotNull(tagsCollection);
        }

        /// <summary>
        /// Tests the count on the <see cref="Directory.GetTags(int)"/> method to ensure it limits the number of
        /// results.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task TestGetTagsCount()
        {
            var requestedCount = 5;
            var tagsCollection = await client.Directory.GetTags(requestedCount);
            Assert.IsNotNull(tagsCollection);
            Assert.IsTrue(tagsCollection.Count() <= requestedCount);
        }

        /// <summary>
        /// Tests the <see cref="Directory.GetTopPodcasts(int)"/> method.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task TestGetTopPodcasts()
        {
            var topPodcastCollection = await client.Directory.GetTopPodcasts(50);
            Assert.IsNotNull(topPodcastCollection);
        }

        /// <summary>
        /// Tests the <see cref="Directory.GetPodcastsWithTag(string, int)"/> method.
        /// </summary>
        /// <param name="tag">The tag for which to search for podcasts.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [DataTestMethod]
        [DataRow("information")]
        [DataRow("mac")]
        [DataRow("new")]
        [DataRow("activism")]
        public async Task TestGetPodcastsWithTag(string tag)
        {
            var podcastCollection = await client.Directory.GetPodcastsWithTag(tag);
            Assert.IsNotNull(podcastCollection);
        }

        /// <summary>
        /// Tests the count on the <see cref="Directory.GetPodcastsWithTag(string, int)"/> to ensure it limits
        /// the number of results.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task TestGetPodcastsWithTagCount()
        {
            var podcastCollection = await client.Directory.GetPodcastsWithTag("information", count: 1);
            Assert.IsNotNull(podcastCollection);
            Assert.IsTrue(podcastCollection.Count() <= 1);
        }

        /// <summary>
        /// Tests the <see cref="Directory.GetPodcastData(string, int)"/> method.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task TestGetPodcastData()
        {
            string url = "http://joeroganexp.joerogan.libsynpro.com/rss";
            var podcast = await client.Directory.GetPodcastData(url);
            Assert.IsNotNull(podcast);
        }

        /// <summary>
        /// Tests the <see cref="Directory.GetPodcastEpisode(string, string)"/> method.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task TestGetEpisodeData()
        {
            var podcastUrl = "http://joeroganexp.joerogan.libsynpro.com/rss";
            var episodeUrl = "http://traffic.libsyn.com/joeroganexp/p1418.mp3?dest-id=19997";
            var episode = await client.Directory.GetPodcastEpisode(podcastUrl, episodeUrl);
            Assert.IsNotNull(episode);
        }

        /// <summary>
        /// Tests the <see cref="Directory.SearchForPodcasts(string, int)"/> method.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task TestSearchForPodcasts()
        {
            var searchQuery = "joe rogan";
            var podcastCollection = await client.Directory.SearchForPodcasts(searchQuery);
            Assert.IsNotNull(podcastCollection);
        }
    }
}
