# Warden.Watchers.AzureStorage
Azure storage watcher for Warden

For information on how to setup your Warden please refer to the [Warden documentation](https://github.com/warden-stack/Warden)

**Azure Storage Watcher** can be used to run specific queries on the storage (for example, validating a table or entity exists, a BLOB exists etc). By default the watcher only created a `CloudStorageClient` for further use by the consumer of the watcher.

Installation:
---
Available as a [NuGet package]().
```
Install-Package Warden.Watchers.AzureStorage
```

Configuration:
---
- EnsureThat() - predicate containing a `CloudStorageClient` that can be used to ensure the table storage is alive.
- EnsureThatAsync() - async - predicate containing a `CloudStorageClient` that can be used to ensure the table storage is alive.

**Azure Storage Watcher** can be configured by using the **AzureStorageWatcherConfiguration** class or via the lambda expression passed to a specialized constructor.

Example of adding the watcher directly to the **Warden** via one of the extension methods:
```
var wardenConfiguration = WardenConfiguration
    .Create()
    .AddAzureStorageWatcher("Storage account", "connectionString", cfg =>
    {
        cfg.EnsureThat(
          storageAccount => {
            var tableClient = storageAccount.CreateCloudTableClient();
            return tableClient.GetTableReference("ExampleTable").Exists();
          }
        )
    });
```
