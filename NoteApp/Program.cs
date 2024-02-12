#region Register DI dependencies

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddDbContext<NoteAppContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Needed for API access through the JS client; see UseCors()
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORS_CONFIG", cors =>
    {
        cors.WithMethods("*")
            .WithHeaders("*")
            .WithOrigins("*");
    });
});

#endregion


#region Application Startup

var app = builder.Build();

// Add for cshtml page mappings
app.MapRazorPages();
app.MapControllers();
app.UseStaticFiles();

app.UseCors("CORS_CONFIG");

// startup server
app.Run();

#endregion
