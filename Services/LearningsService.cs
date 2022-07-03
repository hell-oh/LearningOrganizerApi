using LearningOrganizerApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LearningOrganizerApi.Services;

public class LearningsService
{
    private readonly IMongoCollection<Learning> _learningsCollection;

    public LearningsService(
        IOptions<LearningOrganizerDBSettings> LearningOrganizerDBSettings)
    {
        var mongoClient = new MongoClient(
            LearningOrganizerDBSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            LearningOrganizerDBSettings.Value.DatabaseName);

        _learningsCollection = mongoDatabase.GetCollection<Learning>(
            LearningOrganizerDBSettings.Value.LearningsCollectionName);
    }

    public async Task<List<Learning>> GetAsync() =>
        await _learningsCollection.Find(x => x.Public == true).ToListAsync();


    //Metodo de prueba, es más eficiente obtener desde el mismo usuario a menos que se quiera obtener datos adicionales de todos los learnings del usuario
    public async Task<List<Learning>> GetByUserAsync(string id) =>
        await _learningsCollection.Find(x => x.UserId == id).ToListAsync();

    public async Task<Learning?> GetAsync(string id) =>
        await _learningsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Learning newLearning) =>
        await _learningsCollection.InsertOneAsync(newLearning);

    public async Task UpdateAsync(string id, Learning updatedLearning) =>
        await _learningsCollection.ReplaceOneAsync(x => x.Id == id, updatedLearning);

    public async Task RemoveAsync(string id) =>
        // FilterDefinition<Learning> filter = Builders<Learning>.Filter.Eq(x => x.Id, id);
        await _learningsCollection.DeleteOneAsync(x => x.Id == id);

    public async Task RemoveAllAsync() =>
        await _learningsCollection.DeleteManyAsync(_ => true);
    
    // public async Task AddNoteToLearningAsync(string id, Note newNote)
    // {
    //     FilterDefinition<Learning> filter = Builders<Learning>.Filter.Eq(x => x.Id, id);
    //     UpdateDefinition<Learning> update = Builders<Learning>.Update.AddToSet<string>(x => x.Notes, updatedLearning.Notes);
    //     await _learningsCollection.UpdateOneAsync(filter, update);
        
    //     // var learning = await GetAsync(id);
    //     // if (learning is null)
    //     // {
    //     //     return;
    //     // }
    //     // learning.Notes.Add(newNote);
    //     // await UpdateAsync(id, learning);
    // }
}