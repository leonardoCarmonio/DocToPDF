
using System.Net;
using Web.Models;
using Web.Services.Interfaces;

namespace Web.Services;
public class UploadService : IUploadServices
{
    public async Task<UploadResult> UploadDocument(IFormFile file, IWebHostEnvironment webHostEnvironment)
    {
        var allowedExtensions = new string[] { ".doc",".docx",".odt" };

        var fileInfo = new FileInfo(file.FileName);

        if (!allowedExtensions.Contains(fileInfo.Extension))
        {
            return new UploadResult(
                IsSuccess: false,
                Message: "Extensão não permitida.",
                StatusCode: HttpStatusCode.BadRequest,
                FileName: string.Empty);
        }

        string fileName = Path.Combine("pdf",$"{DateTime.Now.Ticks}{fileInfo.Extension}");
        string filePath = Path.Combine(webHostEnvironment.WebRootPath,fileName);

        try
        {
            using var fileStream = File.Open(filePath,
                                             FileMode.Create,
                                             FileAccess.Write,
                                             FileShare.ReadWrite);

            await file.CopyToAsync(fileStream);

            return new UploadResult(
                IsSuccess: true, 
                Message: "Upload realizado com sucesso", 
                StatusCode: HttpStatusCode.OK, 
                fileName);
        }
        catch (UnauthorizedAccessException)
        {
            return new UploadResult(
                IsSuccess: false, 
                Message: "Diretório de upload não autorizado.", 
                StatusCode: HttpStatusCode.InternalServerError, 
                FileName: string.Empty);
        }
        catch (Exception)
        {
            return new UploadResult(
                IsSuccess: false, 
                Message: "Erro ao fazer upload para o diretório.", 
                StatusCode: HttpStatusCode.InternalServerError, 
                FileName: string.Empty);
        }
    }
}