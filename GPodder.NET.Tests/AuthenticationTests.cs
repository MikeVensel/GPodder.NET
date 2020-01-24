// <copyright file="AuthenticationTests.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET.Tests
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for the <see cref="Authentication"/> class.
    /// </summary>
    [TestClass]
    public class AuthenticationTests
    {
        private readonly string username = "yourusername";
        private readonly string password = "yourpassword";

        /// <summary>
        /// Tests the <see cref="Authentication.Login(string, string)"/> method.
        /// </summary>
        [TestMethod]
        public void TestLogin()
        {
            var client = new GPodderClient();
            Task.Run(async () =>
            {
                try
                {
                    await client.Authentication.Login(this.username, this.password);
                }
                catch (Exception e)
                {
                    Assert.Fail($"The following exception was thrown. {e.Message}");
                }
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
                try
                {
                    await client.Authentication.Login(this.username, this.password);
                    await client.Authentication.Logout(this.username);
                }
                catch (Exception e)
                {
                    Assert.Fail($"The following exception was thrown. {e.Message}");
                }
            }).GetAwaiter().GetResult();
        }
    }
}
