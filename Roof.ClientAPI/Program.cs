using Roof.ClientAPI.Handlers;
using Roof.ClientAPI.Middleware;
using Roof.ClientAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("AuthSettings"));
builder.Services.AddScoped<SetBearerTokenHandler>();
builder.Services.AddHttpClient("EmployeeApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetSection("BaseAddressURL").Value);
}).AddHttpMessageHandler<SetBearerTokenHandler>();

builder.Services.AddControllers();
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

app.UseMiddleware<ExceptionHandleMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();