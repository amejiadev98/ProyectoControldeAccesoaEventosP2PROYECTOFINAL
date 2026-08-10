using ControlAccesoEventos.Blazor.Components;
using ControlAccesoEventos.Blazor.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
// Configure HttpClient for API access. Set ApiBaseUrl in configuration if different.
// Default to the API project's HTTPS launch URL (from ControlAccesoEventos.API/Properties/launchSettings.json)
// Updated to match the API launchSettings (https://localhost:7020/)
var apiBase = builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7020/";
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(apiBase);
});

// Register typed services that receive HttpClient via IHttpClientFactory
builder.Services.AddScoped(sp => sp.GetRequiredService<System.Net.Http.IHttpClientFactory>().CreateClient("ApiClient"));
builder.Services.AddScoped<EventoService>();
builder.Services.AddScoped<InvitadoService>();
builder.Services.AddScoped<EntradaService>();
builder.Services.AddScoped<ValidacionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
