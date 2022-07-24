using CleanArchitecture.Infrastructure.RestClient;
using CleanArchitecture.WasmUI.Models.Options;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddHttpClients(this IServiceCollection services, WebAssemblyHostConfiguration config)
    {
        services.AddSingleton(async provider =>
        {
            var httpClient = provider.GetRequiredService<HttpClient>();
            return await httpClient.GetFromJsonAsync<Settings>("settings.json")
                .ConfigureAwait(false);
        });

        services.AddHttpClient<TodoItemsClient>((provider, client) =>
        {
            var settingsTask = provider.GetRequiredService<Task<Settings>>();
            var settings = settingsTask.Result;

            client.BaseAddress = settings.BaseAddress;
        });

        //services.AddHttpClient<TodoListsClient>(ConfigureClient);
        //services.AddHttpClient<WeatherForecastClient>(ConfigureClient);

        return services;
    }
}
