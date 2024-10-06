using Web.Models;

namespace Web.Services;

public interface IUploadServices
{
    Task<UploadResult> UploadDocument(IFormFile file, IWebHostEnvironment webHostEnvironment);
}