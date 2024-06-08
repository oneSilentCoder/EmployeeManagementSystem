using EmployeeAppServices.Defenitions;
using EmployeeAppServices.Implementations;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Dependancy Injection

//AddHttpClient is specifically designed for configuring and managing HttpClient instances, ensuring efficient resource use and enabling detailed configurations.

//JsonPlaceHolderApi this is the name of the http client service. Using this name client service is created
builder.Services.AddHttpClient<IBlogPostService, BlogPostService>("JsonPlaceHolderApi",client =>
{
    client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});
builder.Services.AddScoped<IBlogPostService, BlogPostService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
