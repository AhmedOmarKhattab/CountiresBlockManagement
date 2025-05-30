using BlockCountriesTask.IServices;
using BlockCountriesTask.MiddleWares;
using BlockCountriesTask.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IIpLookupService, IpLookupService>();
//builder.Services.AddScoped<ICountryBlockService, CountryBlockService>();
builder.Services.AddSingleton<ICountryBlockService, CountryBlockService>();

builder.Services.AddSingleton<ITemporalBlockService, TemporalBlockService>();
builder.Services.AddSingleton<IBlockedLogService, BlockedLogService>();
builder.Services.AddHostedService<TempBlockCleanupService>();

builder.Services.AddCors(
           option => {
               option.AddPolicy("MyPolicy", options => {
                   options.AllowAnyHeader().
                    AllowAnyMethod()
                  .AllowAnyOrigin();
               });
           });

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}
app.UseStatusCodePagesWithReExecute("/errors/{0}");
app.UseCors("MyPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
