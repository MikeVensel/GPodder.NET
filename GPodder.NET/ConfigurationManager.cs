// <copyright file="ConfigurationManager.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET
{
    using System.Text.Json;
    using System.Threading.Tasks;
    using System.Timers;
    using GPodder.NET.Models;

    /// <summary>
    /// Stores configuration information for gPodder.
    /// </summary>
    public class ConfigurationManager
    {
        private const string ConfigRequestUrl = "https://gpodder.net/clientconfig.json";
        private Task<ClientConfig> updateConfigurationTask;
        private Timer configUpdateTimer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationManager"/> class.
        /// </summary>
        public ConfigurationManager()
        {
            this.Init();
        }

        /// <summary>
        /// Retrieve the client config task.
        /// </summary>
        /// <returns>A <see cref="ClientConfig"/> <see cref="Task"/> representing the current client configuration.</returns>
        public Task<ClientConfig> GetConfigTask()
        {
            return this.updateConfigurationTask;
        }

        private async void Init()
        {
            this.updateConfigurationTask = this.ReloadClientConfig();
            var clientConfig = await this.updateConfigurationTask;
            this.configUpdateTimer = new Timer(clientConfig.UpdateTimeoutMilliseconds);
            this.configUpdateTimer.Elapsed += this.ConfigUpdateTimer_Elapsed;
            this.configUpdateTimer.Start();
        }

        private async void ConfigUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.configUpdateTimer.Stop();
            if (this.updateConfigurationTask != null)
            {
                await this.updateConfigurationTask;
            }

            this.updateConfigurationTask = this.ReloadClientConfig();
            var clientConfig = await this.updateConfigurationTask;
            this.configUpdateTimer.Interval = clientConfig.UpdateTimeoutMilliseconds;
            this.configUpdateTimer.Start();
        }

        private async Task<ClientConfig> ReloadClientConfig()
        {
            var clientConfigResponse = await Utilities.HttpClient.GetAsync(ConfigRequestUrl);
            if (!clientConfigResponse.IsSuccessStatusCode)
            {
                // todo maybe log that there was an issue
                return new ClientConfig()
                {
                    MyGpo = new MyGpo()
                    {
                        BaseUrl = "https://gpoder.net/",
                    },
                    MyGpoFeedService = new MyGpoFeedService()
                    {
                        BaseUrl = "http://feeds.gpodder.net/",
                    },
                    UpdateTimeout = 604800,
                };
            }

            var clientConfigContentStream = await clientConfigResponse.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<ClientConfig>(clientConfigContentStream);
        }
    }
}
