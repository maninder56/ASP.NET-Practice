using Microsoft.AspNetCore.HttpLogging; 

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add Http Logging 
builder.Services.AddHttpLogging(configureOptions => configureOptions.LoggingFields =
    HttpLoggingFields.RequestProperties | HttpLoggingFields.ResponseHeaders);
builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging", LogLevel.Information); 

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
switch (app.Environment.IsDevelopment())
{
    case true:
        app.UseHttpLogging(); 
        app.UseDeveloperExceptionPage();
        break;
    case false:
        break; 
}


app.UseHttpsRedirection();

app.MapGet("/", () => "School Api Home"); 

app.Run();
