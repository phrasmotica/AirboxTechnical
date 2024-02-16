using AirboxTechnical.Data.Services;
using AirboxTechnical.Models;
using AirboxTechnical.Validation;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.FullName);
});

// TODO: implement services with a persistent data source, e.g. SQL database, Cosmos DB, table storage
builder.Services.AddSingleton<IUserService, DefaultUserService>();
builder.Services.AddSingleton<IUserLocationService, DefaultUserLocationService>();

builder.Services.AddSingleton<IValidator<CreateUserLocationRequest>, CreateUserLocationRequestValidator>();
builder.Services.AddSingleton<IValidator<CreateUserRequest>, CreateUserRequestValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
