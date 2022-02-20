using Microsoft.AspNetCore.Builder;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Api.Infrastructure
{
    public static class CorsSetup
    {
        private const string OriginsKey = "Origins";

        public static IApplicationBuilder UseCors(this IApplicationBuilder app, IConfiguration configuration)
        {
            var origins = configuration.GetArraySection(OriginsKey);
            app.UseCors(builder =>
            {
                builder.WithOrigins(origins)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });

            return app;
        }

        public static string[] GetArraySection(this IConfiguration configuration, string section)
            => configuration.GetSection(section)
                .GetChildren()
                .ToArray()
                .Select(x => x.Value)
                .ToArray();
    }

}