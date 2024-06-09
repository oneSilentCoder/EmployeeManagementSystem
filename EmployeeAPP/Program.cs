using EmployeeAPP.Helper.Routing;
using EmployeeAppServices.Defenitions.BlogPost;
using EmployeeAppServices.Defenitions.BlogUser;
using EmployeeAppServices.Implementations.BlogPostService;
using EmployeeAppServices.Implementations.BlogUserService;
using EmployeeAppUtilities.Configuration;

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
var jsonPlaceholderBaseUrl = configuration["JsonPlaceholder:BaseUrl"];

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

//==============================================================//
//Dependancy Injection
//==============================================================//

//AddHttpClient is specifically designed for configuring and
//managing HttpClient instances, ensuring efficient resource use
//and enabling detailed configurations.

//JsonPlaceHolderApi this is the name of the http client service.
//Using this name client service is created
//==============================================================//
builder.Services.AddHttpClient<IBlogPostService, BlogPostService>("JsonPlaceHolderApi", client =>
{
    client.BaseAddress = new Uri(jsonPlaceholderBaseUrl);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});
builder.Services.AddHttpClient<IBlogUserService, BlogUserService>("JsonPlaceHolderApi", client =>
{
    client.BaseAddress = new Uri(jsonPlaceholderBaseUrl);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});
builder.Services.AddScoped<IBlogPostService, BlogPostService>();
builder.Services.AddScoped<IBlogUserService, BlogUserService>();
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(Endpoint =>
{
    RouteProvider.Configure(Endpoint);
});

app.Run();
