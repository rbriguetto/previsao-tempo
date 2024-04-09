using Microsoft.EntityFrameworkCore;
using PrevisaoTempo.Infraestructure;
using PrevisaoTempo.Infraestructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPrevisaoTempoInfraestructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PrevisaoTempoDbContext>();
    dbContext.Database.Migrate();
}

app.Run();