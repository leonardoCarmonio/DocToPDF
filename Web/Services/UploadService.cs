
using System.Net;
using Web.Models;

namespace Web.Services;
public class UploadService : IUploadServices
{
    public async Task<UploadResult> UploadDocument(IFormFile file)
    {
        var allowedExtensions = new string[]
        {
            ".doc",
            ".docx",
            ".odt"
        };

        var fileInfo = new FileInfo(file.FileName);

        if (!allowedExtensions.Contains(fileInfo.Extension))
            return new UploadResult(IsSuccess: false, Message: "Extensão não permitida.", statusCode: HttpStatusCode.BadRequest);

        string newFileName = $"uploads/{DateTime.Now.Ticks}{fileInfo.Extension}";

        try
        {
            await file.CopyToAsync(File.OpenWrite(newFileName));
            return new UploadResult(IsSuccess: true, Message: "Upload realizado com sucesso", statusCode: HttpStatusCode.OK);
        }
        catch (UnauthorizedAccessException)
        {
            return new UploadResult(IsSuccess: false, Message: "Diretório de upload não autorizado.", statusCode: HttpStatusCode.InternalServerError);
        }
        catch (Exception)
        {
            return new UploadResult(IsSuccess: false, Message: "Erro ao fazer upload para o diretório.", statusCode: HttpStatusCode.InternalServerError);
        }
    }
}