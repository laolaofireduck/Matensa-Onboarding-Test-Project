using Ewallet.Core;
using Ewallet.Endpoints;
using Ewallet.Web.Mappings;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.MapType<DateOnly>(() => new OpenApiSchema { Type = "string", Format = "date" });
    }
    );
}

{
    builder.Services
        .AddCore()
        .AddEndpoints()
        .AddMappings();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseEndpoints();
    app.UseHttpsRedirection();
}

app.MapControllers();
app.Run();

