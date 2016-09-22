using System;
using Warden.Core;

namespace Warden.Watchers.AzureStorage
{
    public static class Extensions
    {
        public static WardenConfiguration.Builder AddAzureStorageWatcher(this WardenConfiguration.Builder builder, string connectionString,
            Action<WatcherHooksConfiguration.Builder> hooks = null, TimeSpan? interval = null, string group = null)
        {
            builder.AddWatcher(AzureStorageWatcher.Create(connectionString, @group: group), hooks, interval);
            return builder;
        }

        public static WardenConfiguration.Builder AddAzureStorageWatcher(this WardenConfiguration.Builder builder, string name,
            string connectionString, Action<WatcherHooksConfiguration.Builder> hooks = null, TimeSpan? interval = null,
            string group = null)
        {
            builder.AddWatcher(AzureStorageWatcher.Create(name, connectionString, @group: group), hooks, interval);
            return builder;
        }

        public static WardenConfiguration.Builder AddAzureStorageWatcher(this WardenConfiguration.Builder builder, string connectionString, Action<AzureStorageWatcherConfiguration.Default> configurator, Action<WatcherHooksConfiguration.Builder> hooks = null, TimeSpan? interval = null,
            string group = null)
        {
            builder.AddWatcher(AzureStorageWatcher.Create(connectionString, configurator, @group: group), hooks, interval);
            return builder;
        }

        public static WardenConfiguration.Builder AddAzureStorageWatcher(this WardenConfiguration.Builder builder, string name, string connectionString, Action<AzureStorageWatcherConfiguration.Default> configurator, Action<WatcherHooksConfiguration.Builder> hooks = null, TimeSpan? interval = null,
            string group = null)
        {
            builder.AddWatcher(AzureStorageWatcher.Create(name, connectionString, configurator, @group: group), hooks, interval);
            return builder;
        }
    }
}
