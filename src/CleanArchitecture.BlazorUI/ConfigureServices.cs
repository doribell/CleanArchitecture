using CleanArchitecture.BlazorUI.Models.Options;
using CleanArchitecture.Infrastructure.RestClient;
using Microsoft.Extensions.Options;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<Settings>(config.GetSection("Test"));

        services.AddHttpClient<TodoItemsClient>(ConfigureClient);
        services.AddHttpClient<TodoListsClient>(ConfigureClient);
        services.AddHttpClient<WeatherForecastClient>(ConfigureClient);

        return services;
    }

    private static void ConfigureClient(IServiceProvider provider, HttpClient client)
    {
        var settings = provider.GetRequiredService<IOptions<Settings>>().Value;
        client.BaseAddress = settings.BaseAddress;
    }
}
