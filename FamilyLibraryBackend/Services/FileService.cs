namespace FamilyLibraryBackend.Services;

public class FileService
{
    private readonly string _storagePath;

    public FileService(IConfiguration configuration)
    {
        _storagePath = configuration["StorageSettings:FamilyStoragePath"] ?? "storage/families";
    }

    public async Task<(string fileName, string filePath)> SaveFamilyFileAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("Файл не может быть пустым.");

        var filePath = Path.Combine(_storagePath, file.FileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return (file.FileName, filePath);
    }
    
    public string GetFamilyFilePath(string fileName)
    {
        var filePath = Path.Combine(_storagePath, fileName);
        return File.Exists(filePath) ? filePath : throw new FileNotFoundException("Файл не найден.");
    }
}