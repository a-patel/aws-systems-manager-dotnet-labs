#region Imports
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Reflection;
#endregion

namespace AWSSystemsManagerLabs.Web.Extensions
{
    /// <summary>
    /// Swagger extensions.
    /// </summary>
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Add Custom Swagger services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "AWS Systems Manager (with .NET) Labs",
                    Description = "AWS AWS Systems Manager - Parameter Store (with .NET 5.x) Labs",
                    TermsOfService = new Uri("https://github.com/a-patel/aws-systems-manager-dotnet-labs/blob/master/LICENSE"),
                    Contact = new OpenApiContact { Name = "Ashish Patel", Email = "toaashishpatel@gmail.com", Url = new Uri("https://aashishpatel.netlify.app/") },
                    License = new OpenApiLicense { Name = "LICENSE", Url = new Uri("https://github.com/a-patel/aws-systems-manager-dotnet-labs/blob/master/LICENSE") }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                //// Set the comments path for the Swagger JSON and UI. For all libraries
                //var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
                //xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));

                options.DescribeAllEnumsAsStrings();
                options.IgnoreObsoleteProperties();
                options.IgnoreObsoleteActions();
            });

            return services;
        }

        /// <summary>
        /// Use Custom Swagger services
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "AWS Systems Manager (with .NET) Labs API (V1)");
                options.DocumentTitle = "AWS Systems Manager (with .NET) Labs";
                options.DocExpansion(DocExpansion.None);
                options.DisplayRequestDuration();
            });

            return app;
        }
    }
}
