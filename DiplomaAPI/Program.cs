using DiplomaAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Chemical Web API",
        Description = "An ASP.NET Core Web API for managing of descriptions of chemical analysis.",
        Contact = new OpenApiContact
        {
            Name = "Petro Kryvorotko",
            Email = "pkryvorotko@knu.ua"
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddDbContext<UserDbContext>(options => {
    //options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
    var connectionString = builder.Configuration.GetConnectionString("MySqlServer");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();
app.Urls.Add("http://192.168.1.111:5025");
app.Urls.Add("http://localhost:5087");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "ChemWebAPIv1");
        options.RoutePrefix = string.Empty;
    });
}

//if (!app.Environment.IsDevelopment())
//{
//    app.UseHttpsRedirection();
//}

app.UseAuthorization();

app.MapControllers();

app.Run();
