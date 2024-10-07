using System.Net;
using Microsoft.AspNetCore.Mvc;
using Web.Services;
using Web.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IUploadServices, UploadService>();
builder.Services.AddScoped<ISendMessage, SendMessage>();

var app = builder.Build();

app.UseStaticFiles();

app.MapPost("/upload", async ([FromForm] IFormFile file, 
                              [FromServices] IUploadServices uploadServices,
                              [FromServices] ISendMessage sendMessage) =>
{
    var uploadResult = await uploadServices.UploadDocument(file, builder.Environment);

    if (uploadResult.StatusCode is HttpStatusCode.OK) 
    {
        sendMessage.Send(uploadResult.FileName);
        return Results.Ok(uploadResult);
    }
    else
        return Results.Problem(title: uploadResult.Message, statusCode: (int)uploadResult.StatusCode);

}).DisableAntiforgery();

app.Run();
