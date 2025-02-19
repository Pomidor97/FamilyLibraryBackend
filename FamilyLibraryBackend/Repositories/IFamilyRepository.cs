using MongoDB.Driver;

namespace FamilyLibraryBackend.Repositories;

public interface IFamilyRepository
{
    Task<List<FamilyMetadata>> GetAllAsync();
    Task<FamilyMetadata> GetAsync(string id);
    Task AddAsync(FamilyMetadata family);
    Task<DeleteResult> DeleteAsync(string id);
    Task<UpdateResult> UpdateAsync(string id, FamilyMetadata updatedFamily);
}