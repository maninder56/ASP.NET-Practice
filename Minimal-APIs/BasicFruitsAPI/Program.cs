using BasicFruitsAPI;
using BasicFruitsAPI.ConfigurationClasses;
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

if (builder.Environment.IsDevelopment())
{
    // Loading configurations 
    builder.Configuration.Sources.Clear(); // Clear default configuration sources
    builder.Configuration.AddJsonFile(
        "appsettings.Development.json", 
        optional: true, 
        reloadOnChange: true);
    builder.Configuration.AddUserSecrets<Program>();
    
    // convert configuration to POCO 
    builder.Services.Configure<UserOne>(
        builder.Configuration.GetSection("UserOne"));
    builder.Services.Configure<ExternalAPI>(
        builder.Configuration.GetSection("ExternalAPI"));
    builder.Services.Configure<MockConfigurations>(
        builder.Configuration.GetSection("MockConfigurations")); 

    // Addign Open API description 
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(); 
}

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

    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();

// Adds Problem Detail Body to all error status code that don't have body
app.UseStatusCodePages();

app.UseStaticFiles();
app.UseRouting();

// Error handling endpoint
app.MapGet("/error", () => "Sorry, There was an error processing your request");

app.MapGet("/", () => "Hello World");

// Adding Fruits Endpoints
app.AddFuitsEndpoints();

// Adding Configuration Endpoints
app.AddConfigurationEndpoints(); 

app.Run();