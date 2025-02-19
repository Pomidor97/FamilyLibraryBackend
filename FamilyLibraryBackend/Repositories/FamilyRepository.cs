using MongoDB.Bson;
using MongoDB.Driver;

namespace FamilyLibraryBackend.Repositories;

public class FamilyRepository : IFamilyRepository
{
    private readonly IMongoCollection<FamilyMetadata> _families;

    public FamilyRepository(IMongoClient mongoClient, IConfiguration configuration)
    {
        var database = mongoClient.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
        _families = database.GetCollection<FamilyMetadata>(configuration["DatabaseSettings:FamiliesCollectionName"]);
    }

    public async Task<List<FamilyMetadata>> GetAllAsync()
    {
        return await _families.Find(_ => true).ToListAsync();
    }
    
    public async Task<FamilyMetadata> GetAsync(string id)
    {
        if (!ObjectId.TryParse(id, out var objectId))
        {
            return null;
        }

        var filter = Builders<FamilyMetadata>.Filter.Eq("_id", objectId);
        return await _families.Find(filter).FirstOrDefaultAsync();
    }


    public async Task AddAsync(FamilyMetadata family)
    {
        await _families.InsertOneAsync(family);
    }
    
    public async Task<DeleteResult> DeleteAsync(string id)
    {
        return await _families.DeleteOneAsync(f => f.Id == id);
    }

    public async Task<UpdateResult> UpdateAsync(string id, FamilyMetadata updatedFamily)
    {
        var update = Builders<FamilyMetadata>.Update
            .Set(f => f.FamilyName, updatedFamily.FamilyName)
            .Set(f => f.Category, updatedFamily.Category)
            .Set(f => f.Description, updatedFamily.Description);

        return await _families.UpdateOneAsync(f => f.Id == id, update);
    }

}