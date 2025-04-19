using DatabaseContext;
using Microsoft.AspNetCore.HttpLogging;
using SchoolAPI.Data; 

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add Http Logging 
builder.Services.AddHttpLogging(configureOptions => configureOptions.LoggingFields =
    HttpLoggingFields.RequestProperties | HttpLoggingFields.ResponseHeaders);
builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging", LogLevel.Information);

// Add Database Service
builder.Services.AddSchoolDatabaseService();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
switch (app.Environment.IsDevelopment())
{
    case true:
        app.UseHttpLogging(); 
        app.UseDeveloperExceptionPage();
        break;
    case false:
        app.UseExceptionHandler("/error");
        break; 
}


app.UseHttpsRedirection();

//app.UseStaticFiles();
app.UseRouting();   

app.MapGet("/", () => "School Api Home");
app.MapGet("/error", () => "Error occured while processign your request");
app.MapGet("/exception", () => { throw new Exception("This is an Intentional Exception"); } );

app.Run();
