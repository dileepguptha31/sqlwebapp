using webappwithsqldb.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Endpoint=https://tempappconfig.azconfig.io;Id=hbNz;Secret=kJ+9HBAjhnZJIzVg20embttRnzLGwK1i7CZF8EXYoow=";
//Install Microsoft.Extensions.Configuration.AzureAppConfiguration
builder.Host.ConfigureAppConfiguration(builder =>
{
    //Connect to your App Config Store using the connection string
    builder.AddAzureAppConfiguration(connectionString);
});

builder.Services.AddTransient<IProductService, ProductService>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
