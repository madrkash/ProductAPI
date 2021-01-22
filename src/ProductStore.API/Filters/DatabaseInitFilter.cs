using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbUp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProductStore.Infrastructure.Configs;

namespace ProductStore.API.Filters
{
    public class DatabaseInitFilter : IStartupFilter
    {
        private readonly DatabaseConfig _config;
        private readonly ILogger<DatabaseInitFilter> _logger;

        public DatabaseInitFilter(DatabaseConfig config, ILogger<DatabaseInitFilter> logger)
        {
            _config = config;
            _logger = logger;
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            var connectionString = _config.ConnectionString;

            
            EnsureDatabase.For.PostgresqlDatabase(connectionString);

            var dbUpgradeEngineBuilder = DeployChanges.To
                .PostgresqlDatabase(connectionString)
                .LogToAutodetectedLog()
                .WithScriptsEmbeddedInAssembly(typeof(Infrastructure.Data.ProductRepository).Assembly)
                .WithTransaction();

            var dbUpgradeEngine = dbUpgradeEngineBuilder.Build();

            if (dbUpgradeEngine.IsUpgradeRequired())
            {
                _logger.LogInformation("Upgrades have been detected. Upgrading database now...");
                var operation = dbUpgradeEngine.PerformUpgrade();
                if (operation.Successful)
                {
                    _logger.LogInformation("Upgrade completed successfully");
                }
                else
                {
                    _logger.LogError("Error happened in the upgrade. Please check the logs");
                }
            }

            return next;
        }
    }
}