using BusinessLogicLayer;
using DataAccessLayer;
using DataAccessLayer.DatabaseContext;
using DataAccessLayer.Initializer;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBLLLayer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<CrowdfundingDbContext>();
        Initializer.InitializerDb(context);

        var seeder = serviceProvider.GetRequiredService<SeedDb>();
        seeder.SeedLocalEnv();
    }
    catch (Exception)
    {
        throw;
    }

}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
