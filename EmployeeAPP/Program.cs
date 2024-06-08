using EmployeeAPP.Helper.Routing;
using EmployeeAppServices.Defenitions;
using EmployeeAppServices.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

//Dependancy Injection

//AddHttpClient is specifically designed for configuring and managing HttpClient instances, ensuring efficient resource use and enabling detailed configurations.

//JsonPlaceHolderApi this is the name of the http client service. Using this name client service is created
builder.Services.AddHttpClient<IBlogPostService, BlogPostService>("JsonPlaceHolderApi", client =>
{
    client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});
builder.Services.AddScoped<IBlogPostService, BlogPostService>();


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
