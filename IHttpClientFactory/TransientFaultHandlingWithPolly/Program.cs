using Polly;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



//***********************************************************************************************************


builder.Services.AddHttpClient("GoogleClient")
    .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10)));

//we should define policy once and reuse it again  (best practice)
var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10));

builder.Services.AddHttpClient("GoogleClient")
    .AddPolicyHandler(timeoutPolicy);


//transient fault handling 
//this will be retry send request if 5xx status code returned or 408 request timeout status code
builder.Services.AddHttpClient("GitLab")
    .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, x => TimeSpan.FromMilliseconds(300)));


//we use this to prevent duplicate data if we use post method we just use noOp policy
var retryPolicy = HttpPolicyExtensions
  .HandleTransientHttpError()
  .RetryAsync(3);

var noOp = Policy.NoOpAsync().AsAsyncPolicy<HttpResponseMessage>();

builder.Services.AddHttpClient("github")
  .AddPolicyHandler(request => request.Method == HttpMethod.Get ? retryPolicy : noOp);



// policy registry to reuse predefined policies
var registry = builder.Services.AddPolicyRegistry();

var timeout = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10));
var longTimeout = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(30));

registry.Add("regular", timeout);
registry.Add("long", longTimeout);

builder.Services.AddHttpClient("github")
    .AddPolicyHandlerFromRegistry("regular");

//***********************************************************************************************************





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
