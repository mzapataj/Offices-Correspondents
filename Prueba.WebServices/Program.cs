using Microsoft.EntityFrameworkCore;
using Prueba.WebServices.Repository;
using System.Text.Json.Serialization;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


var builder = WebApplication.CreateBuilder(args);


if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.AllowAnyOrigin();
                              policy.AllowAnyHeader();
                              policy.AllowAnyMethod();
                          });
    });
}


builder.Services.AddDbContext<Prueba_ControlBoxContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseSource"));
});

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(MyAllowSpecificOrigins);

app.MapControllers();

app.Run();
