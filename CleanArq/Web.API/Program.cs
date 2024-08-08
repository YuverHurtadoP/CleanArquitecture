using CleanArq;
using Infrastructure;
using Web.API;
using Web.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/*
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();*/

builder.Services.AddPresentation()
.AddInfrastructure(builder.Configuration)
.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.ApplyMigrations();
}

app.UseExceptionHandler("/error");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
