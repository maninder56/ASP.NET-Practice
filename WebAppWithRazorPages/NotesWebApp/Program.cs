using NotesWebApp.Services; 

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IConfigurationSection? seqConfig = builder.Configuration.GetSection("Logging").GetSection("Seq");

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

app.UseStatusCodePagesWithRedirects("/MissingPage/{0}");


app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
