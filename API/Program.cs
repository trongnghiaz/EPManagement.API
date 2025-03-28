using Amazon.SQS;
using Application;
using Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddSerilog(builder.Configuration);

builder.Services.AddCors(options =>
    options.AddPolicy("Cors", p =>
    {
        p.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    })
);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonSQS>();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddAuthen(builder.Configuration);
builder.Services.AddSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Cors");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.AddApplication();

app.Run();
