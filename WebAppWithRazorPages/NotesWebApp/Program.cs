using NotesWebApp.Services; 

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

var seqConfig = builder.Configuration.GetSection("Seq");

var logger = new LoggerFactory().CreateLogger("StartUP");

// Add Logging 
builder.Logging.AddSeq(seqConfig); 

// Add services to the container.
builder.Services.AddRazorPages();

// Custom services 
builder.Services.AddNotesServices();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (seqConfig is null)
{
    logger.LogWarning("Failed to load Seq configurations");
}
else
{
    logger.LogInformation(seqConfig.ToString());
}


app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
