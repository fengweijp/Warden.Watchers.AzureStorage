using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;

namespace Warden.Watchers.AzureStorage
{
    public class AzureStorageWatcher : IWatcher
    {
        private readonly AzureStorageWatcherConfiguration _configuration;
        public string Group { get; }
        public string Name { get; }
        public const string DefaultName = "Azure Storage Watcher";
        public async Task<IWatcherCheckResult> ExecuteAsync()
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_configuration.ConnectionString);
                
                return await EnsureAsync(storageAccount);
            }
            catch (Exception exception)
            {
                throw new WatcherException($"There was an error while trying to access the cloud storage account", exception);
            }
        }
        private async Task<IWatcherCheckResult> EnsureAsync(CloudStorageAccount storageAccount)
        {
            bool isValid = true;
            if (_configuration.EnsureThatAsync != null)
            {
                isValid = await _configuration.EnsureThatAsync?.Invoke(storageAccount);
            }
            isValid = isValid && (_configuration.EnsureThat?.Invoke(storageAccount) ?? true);

            return AzureStorageWatcherCheckResult.Create(this, isValid, storageAccount,
                $"Storage {storageAccount.BlobEndpoint} is checked with result {isValid}");
        }
        protected AzureStorageWatcher(string name, AzureStorageWatcherConfiguration configuration, string group)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Watcher name cannot be empty.");
            }
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration), "Azure Storage Watcher configuration has not been provided.");
            }

            Name = name;
            _configuration = configuration;
            Group = group;
        }

        public static AzureStorageWatcher Create(string name,
                AzureStorageWatcherConfiguration configuration, string group = null)
            => new AzureStorageWatcher(name, configuration, group);
        public static AzureStorageWatcher Create(string connectionString, Action<AzureStorageWatcherConfiguration.Default> configurator = null, string group = null) => Create(DefaultName, connectionString, configurator, group);
        public static AzureStorageWatcher Create(string name, string connectionString,
                Action<AzureStorageWatcherConfiguration.Default> configurator = null, string group = null)
        {
            var config = new AzureStorageWatcherConfiguration.Builder(connectionString);
            configurator?.Invoke((AzureStorageWatcherConfiguration.Default) config);
            return new AzureStorageWatcher(DefaultName, config.Build(), group);
        }

    }
}
