using EmployeeAPP.Helper.Routing;
using EmployeeAppServices.Defenitions.BlogUser;
using EmployeeAppServices.Implementations.BlogUserService;
using EmployeeAppUtilities.Configuration;
using EmployeeAppServices;

var builder = WebApplication.CreateBuilder(args);

// Create a new ConfigurationBuilder and add the custom configuration source
var configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .Add(new CustomConfigurationSource());

// Build the configuration
var configuration = configurationBuilder.Build();

// Add the configuration to the host builder
builder.Configuration.AddConfiguration(configuration);

// Register HttpClient with base URL from custom configuration
var ApiBaseUrl = configuration["JsonPlaceholder:BaseUrl"];
var DummyApiBaseUrl = configuration["DummyApi:BaseUri"];
var DummyApiAppId = configuration["DummyApi:AppId"];


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

//==============================================================//
//Dependancy Injection
//==============================================================//

//AddHttpClient is specifically designed for configuring and
//managing HttpClient instances, ensuring efficient resource use
//and enabling detailed configurations.

//ApiBaseUrl this is the name of the http client service.
//Using this name client service is created
//==============================================================//

builder.Services.EmployeeAppServiceCollection(ApiBaseUrl);
builder.Services.EmployeeAppDummyServiceCollection(DummyApiBaseUrl, DummyApiAppId);

//==============================================================//


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
app.UseStaticFiles(); // Ensure this line is present to serve static files
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(Endpoint =>
{
    RouteProvider.Configure(Endpoint);
});

app.Run();
