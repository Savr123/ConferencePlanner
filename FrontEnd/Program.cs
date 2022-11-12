using FrontEnd.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FrontEnd.Data;
using FrontEnd.Services;
using FrontEnd.Areas.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = "Data Source = security.db";

builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseSqlite(connectionString));


builder.Services.AddSingleton<IAdminService, AdminService>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
    {
        policy.RequireAuthenticatedUser()
              .RequireIsAdminClaim();
    });
});

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddDefaultUI()
    .AddEntityFrameworkStores<IdentityDbContext>()
    .AddClaimsPrincipalFactory<ClaimsPrincipalFactory>();

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Admin", "Admin");
});


builder.Services.AddHttpClient<IApiClient, ApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["serviceUrl"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();;
app.UseAuthorization();

app.MapRazorPages();

app.Run();
