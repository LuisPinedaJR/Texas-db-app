using BlazorApp1;
using BlazorApp1.Components;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to listen on specific IP
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(3389); // Listen on port 3389 on all network interfaces
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configure ServerSettings
builder.Services.Configure<ServerSettings>(
    builder.Configuration.GetSection("ServerSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
