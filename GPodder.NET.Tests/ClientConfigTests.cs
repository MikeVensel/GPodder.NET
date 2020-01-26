// <copyright file="ClientConfigTests.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET.Tests
{
    using System.Text.Json;
    using GPodder.NET.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for the <see cref="ClientConfig"/> class.
    /// </summary>
    [TestClass]
    public class ClientConfigTests
    {
        /// <summary>
        /// Tests the <see cref="ClientConfig"/> serialization to ensure that the json
        /// properties are mapped to the correct objects.
        /// </summary>
        [TestMethod]
        public void TestSerialization()
        {
            var jsonString = "{\"mygpo\":{\"baseurl\":\"https://gpodder.net/\"},\"mygpo-feedservice\":{\"baseurl\":\"http://feeds.gpodder.net/\"},\"update_timeout\":604800}";
            var clientConfig = JsonSerializer.Deserialize<ClientConfig>(jsonString);
            Assert.IsNotNull(clientConfig);
            Assert.IsFalse(string.IsNullOrEmpty(clientConfig.MyGpo.BaseUrl));
            Assert.IsFalse(string.IsNullOrEmpty(clientConfig.MyGpoFeedService.BaseUrl));
            Assert.IsTrue(clientConfig.UpdateTimeout > 0);
            Assert.IsTrue(clientConfig.UpdateTimeoutMilliseconds > 0);
        }

        /// <summary>
        /// Tests the <see cref="ClientConfig.UpdateTimeout"/> and <see cref="ClientConfig.UpdateTimeoutMilliseconds"/>.
        /// </summary>
        [TestMethod]
        public void TestUpdateMilliseconds()
        {
            var clientConfig = new ClientConfig()
            {
                UpdateTimeout = 4,
            };

            Assert.AreEqual(4000, clientConfig.UpdateTimeoutMilliseconds);

            clientConfig.UpdateTimeoutMilliseconds = 5000;
            Assert.AreEqual(5, clientConfig.UpdateTimeout);
        }
    }
}
