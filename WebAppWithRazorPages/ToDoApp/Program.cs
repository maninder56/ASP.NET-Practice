using Microsoft.AspNetCore.HttpLogging;
using ToDoApp.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add logging 
builder.Services.AddHttpLogging(co => co.LoggingFields =
    HttpLoggingFields.RequestProperties | HttpLoggingFields.ResponseHeaders);
builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging", LogLevel.Warning);

// Add Todo Service 
builder.Services.AddTodoService(); 

builder.Services.AddRazorPages();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpLogging(); 

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
