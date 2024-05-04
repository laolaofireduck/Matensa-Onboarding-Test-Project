using Ewallet.Core;
using Ewallet.Endpoints;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddCore()
        .AddEndpoints();
}

var app = builder.Build();
{
    app.UseEndpoints();
    app.UseHttpsRedirection();
}

app.Run();

