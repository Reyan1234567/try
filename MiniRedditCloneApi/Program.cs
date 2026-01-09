using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniRedditCloneApi.Data;
using MiniRedditCloneApi.Data.Configurations;
using MiniRedditCloneApi.Models;
using MiniRedditCloneApi.Services.Implementations;
using MiniRedditCloneApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<MiniRedditCloneApiDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("MiniRedditCloneDb"));
});
builder.Services.AddIdentity<Nerd, IdentityRole>(opt =>
{
    opt.User.RequireUniqueEmail = true;
    opt.Password.RequiredLength = 8;
    opt.Password.RequireDigit = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireUppercase = false;
    opt.SignIn.RequireConfirmedEmail = true;
})
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<MiniRedditCloneApiDbContext>();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddAutoMapper(cfg => cfg.LicenseKey = builder.Configuration.GetConnectionString("AutoMapperLicense"), typeof(Program));
builder.Services.ConfigureHttpJsonOptions(opt => opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var moderationUrl = builder.Configuration["ModerationResult:BaseUrl"] ?? "http://127.0.0.1:8000";
builder.Services.AddHttpClient<INsfwDetectionService, NsfwDetectionService>(client =>
{
    client.BaseAddress = new Uri(moderationUrl);
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<INerdsService, NerdsService>();
builder.Services.AddScoped<IHerdManagementService, HerdManagementService>();
builder.Services.AddScoped<IHerdMembershipService, HerdMembershipService>();
builder.Services.AddScoped<IHerdModerationService, HerdModerationService>();
builder.Services.AddScoped<IHerdDomainService, HerdDomainService>();
builder.Services.AddScoped<IInsightsService, InsightsService>();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Email configuration singleton
builder.Services.AddSingleton(sp =>
    builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>()!
);
builder.Services.AddScoped<IEmailService, EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
# File update Fri, Jan  9, 2026  9:16:22 PM
# Update Fri, Jan  9, 2026  9:26:05 PM
# Update Fri, Jan  9, 2026  9:34:55 PM
