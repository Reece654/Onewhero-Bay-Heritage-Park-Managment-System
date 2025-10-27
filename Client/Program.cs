using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Onewhero_Bay_Heritage_Park_Managment_System.Client;
using Onewhero_Bay_Heritage_Park_Managment_System.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Add SupabaseService first (AuthService depends on it)
builder.Services.AddScoped<SupabaseService>();

// Add AuthService
builder.Services.AddScoped<AuthService>();

await builder.Build().RunAsync();