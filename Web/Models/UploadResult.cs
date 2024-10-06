using System.Net;

namespace Web.Models;

public record UploadResult(bool IsSuccess, string Message, HttpStatusCode StatusCode, string FileName);
