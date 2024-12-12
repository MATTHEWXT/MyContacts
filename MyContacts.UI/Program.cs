using MyContacts.UI.Components;
using MyContacts.UI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7002");
});

builder.Services.AddScoped<ContactService>(provider =>
{
    var clientFactory = provider.GetRequiredService<IHttpClientFactory>();
    return new ContactService(clientFactory.CreateClient("ApiClient"));
});
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
