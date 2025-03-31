using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Logging.Abstractions;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddHttpLogging(configureOptions => 
    configureOptions.LoggingFields = HttpLoggingFields.RequestProperties | HttpLoggingFields.Response);

// Ensures that logs added by the HTTP logging middleware are visible in the log output
builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging", LogLevel.Information);

WebApplication? app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseHttpLogging(); // HTTP logging middleware logs each request to the application in the log output.
}

//app.UseWelcomePage();
app.UseStaticFiles();   

app.MapGet("/", () => "Hello World!");
app.MapGet("/person", () => new Person("Adam", "smith"));   

app.Run();

public record Person(string FirstName, string LastName); 

