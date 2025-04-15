using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.HttpLogging;
using RecipeMinimalAPI;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Adding Http Logging 
builder.Services.AddHttpLogging(configureOptions => configureOptions.LoggingFields =
    HttpLoggingFields.RequestProperties | HttpLoggingFields.ResponseHeaders);

builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging", LogLevel.Information); 


WebApplication app = builder.Build();

// Configure the HTTP request pipeline.

switch(app.Environment.IsDevelopment())
{
    case false:
        app.UseExceptionHandler("/error"); 
        break;

    case true:
        app.UseHttpLogging();
        app.UseDeveloperExceptionPage();
        break;
}

app.UseHttpsRedirection();
app.UseRouting();

// Endpoints 
app.MapGet("/error", () => "Sorry, There was an error while processing your request"); 

app.MapGet("/", () => "Hello world");

app.MapGet("/throwException", () => { throw new Exception("Intentional Exception"); }); 

app.AddRecipeEndpoints();

app.Run();



