using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BidwotaGiri_Dot_Net_Assignment.Areas.Identity.Data;
using BidwotaGiri_Dot_Net_Assignment.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<BidwotaGiri_Dot_Net_AssignmentContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BidwotaGiri_Dot_Net_AssignmentContextConnection")));


builder.Services.AddDefaultIdentity<BidwotaGiri_Dot_Net_AssignmentUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<BidwotaGiri_Dot_Net_AssignmentContext>();



builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<BidwotaGiri_Dot_Net_AssignmentContext>();
        dbContext.Database.Migrate();

    }
    catch (Exception ex)
    {

        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
//------------------------- updated -------------------




