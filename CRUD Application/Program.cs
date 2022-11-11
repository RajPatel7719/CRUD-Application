using CRUD_Application.Models;
using CRUD.ServiceProvider;
using CRUD.ServiceProvider.IService;
using CRUD_Application.Filter;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(config =>
{
    config.Filters.Add(typeof(CustomAuthenticationFilter));
    config.Filters.Add(typeof(CustomExceptionFilter));
}).AddMvcOptions(option =>
    option.Filters.Add(
        new ResponseCacheAttribute
        {
            NoStore = true,
            Location = ResponseCacheLocation.None,
        }));

//builder.Services.AddResponseCaching();
//Add Auto Mapper Service
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(20);
    option.Cookie.HttpOnly = true;
    option.Cookie.IsEssential = true;
});

//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
//    options.LoginPath = "~/Account/Login";
//    options.SlidingExpiration = true;
//});

builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});
var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IApiProvider, ApiProvider>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IImageUpload, ImageUpload>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error/Error");
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/Error/Error");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();
//app.UseResponseCaching();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
