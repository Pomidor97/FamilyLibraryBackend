using FamilyLibraryBackend.Repositories;

public class FamilyService
{
    private readonly IFamilyRepository _familyRepository;

    public FamilyService(IFamilyRepository familyRepository)
    {
        _familyRepository = familyRepository;
    }

    public async Task<List<FamilyMetadata>> GetAllFamiliesAsync()
    {
        return await _familyRepository.GetAllAsync();
    }

    public async Task AddFamilyAsync(FamilyMetadata family)
    {
        if (string.IsNullOrEmpty(family.FamilyName))
            throw new ArgumentException("Название семейства не может быть пустым.");

        family.CreatedAt = DateTime.UtcNow;
        await _familyRepository.AddAsync(family);
    }

    public async Task AddFamilyWithFileAsync(string familyName, string category, string description, string fileName, string filePath)
    {
        var family = new FamilyMetadata
        {
            FamilyName = familyName,
            Category = category,
            Description = description,
            FileName = fileName,
            FilePath = filePath,
            CreatedAt = DateTime.UtcNow
        };

        await _familyRepository.AddAsync(family);
    }
    
    public async Task<bool> DeleteFamilyAsync(string id)
    {
        var metaData = await _familyRepository.GetAsync(id);
        var result = await _familyRepository.DeleteAsync(id);
        File.Delete(metaData.FilePath);
        return result.DeletedCount > 0;
    }

    public async Task<bool> UpdateFamilyAsync(string id, FamilyMetadata updatedFamily)
    {
        var result = await _familyRepository.UpdateAsync(id, updatedFamily);
        return result.ModifiedCount > 0;
    }

    public async Task<FamilyMetadata?> GetFamilyAsync(string id)
    {
        return await _familyRepository.GetAsync(id);
    }
}