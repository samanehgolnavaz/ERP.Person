

using ERP.Person.Middlewares;
using ERP.Person.Services.Implementation;
using ERP.Person.Services.Interfaces;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);
Activity.DefaultIdFormat = ActivityIdFormat.W3C;
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<GlobalExceptionHandlerMiddleware>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IGuidGenerator ,GuidGenerator>();

builder.Services.AddSerilog((provider, configuration) =>
{
    configuration.WriteTo.Console(LogEventLevel.Information,
       "{Timestamp : HH :mm: ss}[{Level}]{Message}{NewLine}{Exception}");
    configuration.WriteTo.File(new JsonFormatter(),"Logs/log.json"
    , LogEventLevel.Information
    , 204800
    , null
    , rollingInterval:RollingInterval.Minute);
});


builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PersonDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
