using DevTest.Interfaces;
using DevTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevTest.Controllers
{
    [Route("api/files")]
    public class FilesController(IFileUploadService uploadService, IOptions<AppSettings> configuration) : Controller
    {
        private readonly IFileUploadService uploadService = uploadService;
        private readonly AppSettings appSettings = configuration.Value;

        [HttpPost]
        public async Task<IActionResult> UploadFileToBlobDirectory(IFormFile file)
        {
            var fileName = file.FileName;

            if (!uploadService.MatchesFileTypeConstraint(fileName))
            {
                return BadImageType();
            }

            if (!uploadService.MatchesFileSizeConstraint(file.Length))
            {
                return FileTooBig();
            }

            var result = await uploadService.UploadFile(file);

            return result == null
                ? InternalIssue()
                : Ok(new FileUploadResponse()
                {
                    Message = "Upload Successful",
                    Data = new()
                    {
                        FileName = result.Value.Key,
                        ImageUrl = result.Value.Value
                    }
                });
        }

        private IActionResult BadImageType()
        {
            return BadRequest(new FileUploadResponse()
            {
                Code = FileUploadErrorCodeEnum.FileTypeConstraintMismatch,
                Message = "Upload Failed",
                Error = new()
                    {
                        "File was not a supported image type"
                    }
            });
        }

        private IActionResult FileTooBig()
        {
            return BadRequest(new FileUploadResponse()
            {
                Code = FileUploadErrorCodeEnum.FileTooLarge,
                Message = "Upload Failed",

                Error = new() { $"File was over {appSettings.MaxFileSizeInMb}mb" }
            });
        }

        private IActionResult InternalIssue()
        {
            return BadRequest(new FileUploadResponse()
            {
                Code = FileUploadErrorCodeEnum.ServerError,
                Message = "Upload Failed",
                Error = new()
                    {
                        "An issue occurred when attempting to upload your image - please try again later."
                    }
            });
        }
    }
}
