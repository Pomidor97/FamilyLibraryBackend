using FamilyLibraryBackend.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/files")]
public class FileController : ControllerBase
{
    private readonly FileService _fileService;
    private readonly FamilyService _familyService;

    public FileController(FileService fileService, FamilyService familyService)
    {
        _fileService = fileService;
        _familyService = familyService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(
        [FromForm] IFormFile file, 
        [FromForm] string category, 
        [FromForm] string familyName, 
        [FromForm] string description)
    {
        try
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { message = "Файл обязателен для загрузки." });

            if (string.IsNullOrEmpty(familyName) || string.IsNullOrEmpty(category) || string.IsNullOrEmpty(description))
                return BadRequest(new { message = "Все поля (familyName, category, description) обязательны." });

            var (fileName, filePath) = await _fileService.SaveFamilyFileAsync(file);

            await _familyService.AddFamilyWithFileAsync(familyName, category, description, fileName, filePath);

            return Ok(new { message = "Файл загружен и сохранен в базе", fileName, filePath });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }


    [HttpGet("download/{fileName}")]
    public IActionResult DownloadFile(string fileName)
    {
        try
        {
            var filePath = _fileService.GetFamilyFilePath(fileName);
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/octet-stream", fileName);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}