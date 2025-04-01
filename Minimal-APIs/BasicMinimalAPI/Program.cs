using Microsoft.AspNetCore.HttpLogging;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddHttpLogging(configureOptions => 
    configureOptions.LoggingFields = HttpLoggingFields.RequestProperties | HttpLoggingFields.ResponsePropertiesAndHeaders);

// Ensures that logs added by the HTTP logging middleware are visible in the log output
builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging", LogLevel.Information);

WebApplication? app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
}

if (app.Environment.IsDevelopment())
{
    app.UseHttpLogging(); // HTTP logging middleware logs each request to the application in the log output.
    app.UseDeveloperExceptionPage(); // only use in development 
}

//app.UseWelcomePage();

app.UseStaticFiles();   
app.UseRouting();

// Error handling 
app.MapGet("/error", () => "Sorry, There was an problem processing your request.");
app.MapGet("testerror", () => GenerateError.ThrowError()); // Testing exeption handling middleware 

app.MapGet("/", () => "Hello World!");
app.MapGet("/person", () => new Person("Adam", "smith"));



app.Run();

record Person(string FirstName, string LastName);

internal class GenerateError
{
    public static string ThrowError() => throw new Exception("Intentional Error"); 
}