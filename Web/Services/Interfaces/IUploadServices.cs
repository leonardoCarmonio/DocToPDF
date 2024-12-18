using Web.Models;

namespace Web.Services.Interfaces;

public interface IUploadServices
{
    Task<UploadResult> UploadDocument(IFormFile file, IWebHostEnvironment webHostEnvironment);
}