using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;

namespace Warden.Watchers.AzureStorage
{
    public class AzureStorageWatcherConfiguration
    {
        public string ConnectionString { get; protected set; }
        public Func<CloudStorageAccount, Task<bool>> EnsureThatAsync { get; protected set; }
        public Func<CloudStorageAccount, bool> EnsureThat { get; protected set; }

        protected internal AzureStorageWatcherConfiguration(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("URL can not be empty.", nameof(connectionString));
            }
            ConnectionString = connectionString;
        }

        public class Default : Configurator<Default>
        {
            public Default(AzureStorageWatcherConfiguration configuration) : base(configuration)
            {
                SetInstance(this);
            }
        }

        public class Configurator<T> : WatcherConfigurator<T, AzureStorageWatcherConfiguration> where T : Configurator<T>
        {
            protected Configurator(string url)
            {
                Configuration = new AzureStorageWatcherConfiguration(url);
            }

            protected Configurator(AzureStorageWatcherConfiguration configuration) : base(configuration)
            {
            }

            public T EnsureThat(Func<CloudStorageAccount, bool> ensureThat)
            {
                if (ensureThat == null)
                {
                    throw new ArgumentException("Ensure that predicate can not be null", nameof(ensureThat));
                }
                Configuration.EnsureThat = ensureThat;
                return Configurator;
            }

            public T EnsureThatAsync(Func<CloudStorageAccount, Task<bool>> ensureThat)
            {
                if (ensureThat == null)
                    throw new ArgumentException("Ensure that async predicate can not be null.", nameof(ensureThat));

                Configuration.EnsureThatAsync = ensureThat;

                return Configurator;
            }
        }

        public class Builder : Configurator<Builder>
        {
            public Builder(string connectionString) : base(connectionString)
            {
                SetInstance(this);
            }

            public AzureStorageWatcherConfiguration Build() => Configuration;

            public static explicit operator Default(Builder builder) => new Default(builder.Configuration);
        }
    }
}