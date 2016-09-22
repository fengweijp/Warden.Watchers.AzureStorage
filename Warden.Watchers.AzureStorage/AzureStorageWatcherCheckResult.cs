using Microsoft.WindowsAzure.Storage;

namespace Warden.Watchers.AzureStorage
{
    public class AzureStorageWatcherCheckResult : WatcherCheckResult
    {
        public CloudStorageAccount StorageAccount { get; set; }

        public AzureStorageWatcherCheckResult(AzureStorageWatcher watcher, bool isValid, string description, CloudStorageAccount storageAccount) : base(watcher, isValid, description)
        {
            StorageAccount = storageAccount;
        }

        public static AzureStorageWatcherCheckResult Create(AzureStorageWatcher watcher, bool isValid,
                CloudStorageAccount storageAccount, string description = "")
            => new AzureStorageWatcherCheckResult(watcher, isValid, description, storageAccount);
    }
}