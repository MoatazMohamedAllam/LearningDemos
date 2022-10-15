using OutgoingRequestMiddlewareWithHandler.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//register our handlers
builder.Services.AddTransient<TimingHandler>();
builder.Services.AddTransient<ValidateHeadersHandler>();

builder.Services.AddHttpClient("MyClient", client =>
{
    client.BaseAddress = new Uri("https://google.com");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Demo");
}).AddHttpMessageHandler<TimingHandler>() // this on the outside and execute first and when response arrive this handler will be last 
.AddHttpMessageHandler<ValidateHeadersHandler>(); //this handler is inside , closest to the request


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
