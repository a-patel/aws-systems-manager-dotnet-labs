#region Imports
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
#endregion

namespace AwsSystemsManagerLabs.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(builder =>
                {
                    builder.AddSystemsManager(configureSource =>
                    {
                        // Parameter Store prefix to pull configuration data from.
                        configureSource.Path = "/myapplication";

                        // Reload configuration data every 5 minutes.
                        configureSource.ReloadAfter = TimeSpan.FromMinutes(5);

                        // Use custom logic to set AWS credentials and Region. Otherwise, the AWS SDK for .NET's default logic
                        // will be used find credentials.
                        //configureSource.AwsOptions = awsOptions;

                        // Configure if the configuration data is optional.
                        configureSource.Optional = true;

                        configureSource.OnLoadException += exceptionContext =>
                        {
                            // Add custom error handling. For example, look at the exceptionContext.Exception and decide
                            // whether to ignore the error or tell the provider to attempt to reload.
                        };

                        // Implement custom parameter process, which transforms Parameter Store names into
                        // names for the .NET Core configuration system.
                        //configureSource.ParameterProcessor = customerProcess;
                    });
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}



#region References
//https://aws.amazon.com/blogs/developer/net-core-configuration-provider-for-aws-systems-manager/
#endregion
