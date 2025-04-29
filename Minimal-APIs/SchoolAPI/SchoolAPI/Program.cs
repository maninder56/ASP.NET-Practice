using DatabaseContext;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Data;
using SchoolAPI.Endpoints;
using SchoolAPI.Services; 

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add Http Logging 
builder.Services.AddHttpLogging(configureOptions => configureOptions.LoggingFields =
    HttpLoggingFields.RequestProperties | HttpLoggingFields.ResponseHeaders);
builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging", LogLevel.Information);

// Add service to convert all exceptions to Problem details 
builder.Services.AddProblemDetails(); 

// Add Database Service
builder.Services.AddSchoolDatabaseService();
builder.Services.AddDatabaseOperationsServices();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
switch (app.Environment.IsDevelopment())
{
    case true:
        app.UseHttpLogging(); 
        app.UseDeveloperExceptionPage();
        break;
    case false:
        app.UseExceptionHandler();
        break; 
}


app.UseHttpsRedirection();

//app.UseStaticFiles();
app.UseRouting();   

// To convert error status code to problem details 
app.UseStatusCodePages();

app.MapGet("/", () => "School Api Home");
app.MapGet("/error", () => "Error occured while processign your request");
app.MapGet("/exception", () => { throw new Exception("This is an Intentional Exception"); } );

app.MapDepartmentEndpoints();
app.MapOnsiteCourseEndpoints();
app.MapOnlineCourseEndpoints();
app.MapCourseEndpoints();
app.MapOfficeAssignmentEndpoints();
app.MapInstructorEndpoints();

app.Run();
