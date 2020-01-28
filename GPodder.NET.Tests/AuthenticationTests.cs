// <copyright file="AuthenticationTests.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET.Tests
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for the <see cref="Authentication"/> class.
    /// </summary>
    [TestClass]
    public class AuthenticationTests
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationTests"/> class.
        /// </summary>
        public AuthenticationTests()
        {
            var configBuilder = new ConfigurationBuilder()
                .AddUserSecrets<AuthenticationTests>();
            this.Configuration = configBuilder.Build();
        }

        private IConfiguration Configuration { get; set; }

        /// <summary>
        /// Tests the <see cref="Authentication.Login(string, string)"/> method.
        /// </summary>
        [TestMethod]
        public void TestLogin()
        {
            var client = new GPodderClient();
            Task.Run(async () =>
            {
                await client.Authentication.Login(this.Configuration["GpodderUsername"], this.Configuration["GpodderPassword"]);
            }).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Tests the <see cref="Authentication.Logout(string)"/> method.
        /// </summary>
        [TestMethod]
        public void TestLogOut()
        {
            var client = new GPodderClient();
            Task.Run(async () =>
            {
                await client.Authentication.Login(this.Configuration["GpodderUsername"], this.Configuration["GPodderPassword"]);
                await client.Authentication.Logout(this.Configuration["GpodderUsername"]);
            }).GetAwaiter().GetResult();
        }
    }
}
