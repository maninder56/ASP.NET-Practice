using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.HttpLogging;
using RecipeMinimalAPI;
using RecipeMinimalAPI.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Adding Http Logging 
builder.Services.AddHttpLogging(configureOptions => configureOptions.LoggingFields =
    HttpLoggingFields.RequestProperties | HttpLoggingFields.ResponseHeaders);

builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging", LogLevel.Information);

builder.Services.AddProblemDetails();

builder.Services.AddRecipeServices(); 

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.

switch(app.Environment.IsDevelopment())
{
    case false:
        app.UseExceptionHandler(); 
        break;

    case true:
        app.UseHttpLogging();
        app.UseDeveloperExceptionPage();
        break;
}

app.UseStatusCodePages();   

app.UseHttpsRedirection();
app.UseRouting();

// Endpoints 
app.MapGet("/error", () => "Sorry, There was an error while processing your request"); 

app.MapGet("/", () => "Hello world");

app.MapGet("/throwException", () => { throw new Exception("Intentional Exception"); }); 

app.AddRecipeEndpoints();

app.Run();



