using WebApplication2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Named Cleint
builder.Services.AddHttpClient(NamedHttpClients.GoogleClient, client =>
{
    client.BaseAddress = new Uri("https://google.com");
    client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
    client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactoryTesting");
});


//Typed Client
builder.Services.AddHttpClient<MyGoogleClient>(client =>
{
    client.BaseAddress = new Uri("https://google.com");
    client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
    client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactoryTesting");
});


//wrapper over httpclient
builder.Services.AddHttpClient<IMyGoogleClient, MyGoogleClient2>(client =>
{
    client.BaseAddress = new Uri("https://google.com");
    client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
    client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactoryTesting");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
