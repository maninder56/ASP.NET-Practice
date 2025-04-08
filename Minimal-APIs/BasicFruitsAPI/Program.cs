using BasicFruitsAPI;
using BasicFruitsAPI.Services;
using Microsoft.AspNetCore.HttpLogging;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpLogging(config => 
    config.LoggingFields = HttpLoggingFields.RequestProperties | HttpLoggingFields.ResponsePropertiesAndHeaders);

// Add Fruit Service 
builder.Services.AddFruitService();

// Ensures that logs added by the HTTP logging middleware are visible in the log output
builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging", LogLevel.Information);

// Adding Problem Detail standard 
builder.Services.AddProblemDetails(); 

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error"); 
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseHttpLogging();
}

app.UseHttpsRedirection();

// Adds Problem Detail Body to all error status code that don't have body
app.UseStatusCodePages();

app.UseStaticFiles();
app.UseRouting();

// Error handling endpoint
app.MapGet("/error", () => "Sorry, There was an error processing your request");

app.MapGet("/", () => "Hello World");

// Fruits Endpoints
app.AddFuitsEndpoints(); 

app.Run();