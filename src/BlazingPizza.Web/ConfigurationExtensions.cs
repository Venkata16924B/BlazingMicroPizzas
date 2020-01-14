using System;

namespace Microsoft.Extensions.Configuration
{
    public static class ConfigurationExtensions
    {
        public static string GetUrl(this IConfiguration configuration, string name)
        {
            var host = configuration.GetHost(name);
            var port = configuration.GetPort(name);
            var protocol = configuration.GetProtocol(name) ?? "http";

            if (string.IsNullOrEmpty(host) || port == null)
            {
                return null;
            }

            return protocol + "://" + host + ":" + port + "/";
        }

        public static string GetHost(this IConfiguration configuration, string name)
        {
            return configuration[$"service:{name}:host"];
        }

        public static int? GetPort(this IConfiguration configuration, string name)
        {
            if (int.TryParse(configuration[$"service:{name}:port"], out var port))
            {
                return port;
            }
            return null;
        }


        public static string GetProtocol(this IConfiguration configuration, string name)
        {
            return configuration[$"service:{name}:protocol"];
        }
    }
}