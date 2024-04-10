using PrevisaoTempo.Infraestructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPrevisaoTempoInfraestructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(options => options
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin());
}

app.MapControllers();
app.UsePrevisaoTempoInfraestructure(app.Services);

app.Run();

public partial class Program { 
    protected Program() {

    }
}