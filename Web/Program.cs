using System.Net;
using Microsoft.AspNetCore.Mvc;
using Web.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IUploadServices, UploadService>();

var app = builder.Build();

app.MapPost("/upload", async ([FromForm] IFormFile file, 
                              [FromServices] IUploadServices uploadServices) =>
{
    var uploadResult = await uploadServices.UploadDocument(file);

    if (uploadResult.statusCode is HttpStatusCode.OK)
        return Results.Ok(uploadResult);
    else
        return Results.Problem(title: uploadResult.Message, statusCode: (int)uploadResult.statusCode);

}).DisableAntiforgery();

app.Run();
