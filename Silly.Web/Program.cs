using System.Reflection;
using System.Text;
using MediatR;
using Mediatr.Web;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Silly.Application.Fruit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(typeof(GetFruit));

var jsonSettings = new JsonSerializerSettings()
{
    TypeNameHandling = TypeNameHandling.Objects,
    SerializationBinder = new KnownTypesBinder(Assembly.GetAssembly(typeof(GetFruit))!)
};

var app = builder.Build();

app.MapPost("/", async (HttpContext http, IMediator mediator, CancellationToken ct) =>
{
    using var streamReader = new HttpRequestStreamReader(http.Request.Body, Encoding.UTF8);

    var json = await streamReader.ReadToEndAsync();

    var request = JsonConvert.DeserializeObject(json, jsonSettings);

    if (request == null)
        throw new Exception();

    return await mediator.Send(request, ct);
});

app.Run();