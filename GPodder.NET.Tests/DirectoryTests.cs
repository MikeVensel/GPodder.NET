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
        /// <summary>
        /// Tests the <see cref="Directory.GetTags(int)"/> with the default count.
        /// </summary>
        [TestMethod]
        public void TestGetTags()
        {
            var client = new GPodderClient();
            Task.Run(async () =>
            {
                var tagsCollection = await client.Directory.GetTags();
                Assert.IsNotNull(tagsCollection);
            }).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Tests the count on the <see cref="Directory.GetTags(int)"/> method to ensure it limits the number of
        /// results.
        /// </summary>
        [TestMethod]
        public void TestGetTagsCount()
        {
            var requestedCount = 5;
            var client = new GPodderClient();
            Task.Run(async () =>
            {
                var tagsCollection = await client.Directory.GetTags(requestedCount);
                Assert.IsNotNull(tagsCollection);
                Assert.IsTrue(tagsCollection.Count() <= requestedCount);
            }).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Tests the <see cref="Directory.GetTopPodcasts(int)"/> method.
        /// </summary>
        [TestMethod]
        public void TestGetTopPodcasts()
        {
            var client = new GPodderClient();
            Task.Run(async () =>
            {
                var topPodcastCollection = await client.Directory.GetTopPodcasts(50);
                Assert.IsNotNull(topPodcastCollection);
            }).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Tests the <see cref="Directory.GetPodcastsWithTag(string, int)"/> method.
        /// </summary>
        /// <param name="tag">The tag for which to search for podcasts.</param>
        [DataTestMethod]
        [DataRow("information")]
        [DataRow("mac")]
        [DataRow("new")]
        [DataRow("activism")]
        public void TestGetPodcastsWithTag(string tag)
        {
            var client = new GPodderClient();
            Task.Run(async () =>
            {
                var podcastCollection = await client.Directory.GetPodcastsWithTag(tag);
                Assert.IsNotNull(podcastCollection);
            }).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Tests the count on the <see cref="Directory.GetPodcastsWithTag(string, int)"/> to ensure it limits
        /// the number of results.
        /// </summary>
        [TestMethod]
        public void TestGetPodcastsWithTagCount()
        {
            var client = new GPodderClient();
            Task.Run(async () =>
            {
                var podcastCollection = await client.Directory.GetPodcastsWithTag("information", count: 1);
                Assert.IsNotNull(podcastCollection);
                Assert.IsTrue(podcastCollection.Count() <= 1);
            }).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Tests the <see cref="Directory.GetPodcastData(string, int)"/> method.
        /// </summary>
        [TestMethod]
        public void TestGetPodcastData()
        {
            string url = "http://joeroganexp.joerogan.libsynpro.com/rss";
            var client = new GPodderClient();
            Task.Run(async () =>
            {
                var podcast = await client.Directory.GetPodcastData(url);
                Assert.IsNotNull(podcast);
            }).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Tests the <see cref="Directory.GetPodcastEpisode(string, string)"/> method.
        /// </summary>
        [TestMethod]
        public void TestGetEpisodeData()
        {
            var podcastUrl = "http://joeroganexp.joerogan.libsynpro.com/rss";
            var episodeUrl = "http://traffic.libsyn.com/joeroganexp/p1418.mp3?dest-id=19997";
            var client = new GPodderClient();
            Task.Run(async () =>
            {
                var episode = await client.Directory.GetPodcastEpisode(podcastUrl, episodeUrl);
                Assert.IsNotNull(episode);
            }).GetAwaiter().GetResult();
        }
    }
}
