using LearningOrganizerApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LearningOrganizerApi.Services;

public class CategoriesService
{
    private readonly IMongoCollection<Category> _categoriesCollection;
    private readonly IMongoCollection<Learning> _learningsCollection;


    public CategoriesService(
        IOptions<LearningOrganizerDBSettings> LearningOrganizerDBSettings)
    {
        var mongoClient = new MongoClient(
            LearningOrganizerDBSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            LearningOrganizerDBSettings.Value.DatabaseName);

        _categoriesCollection = mongoDatabase.GetCollection<Category>(
            LearningOrganizerDBSettings.Value.CategoriesCollectionName);

        _learningsCollection = mongoDatabase.GetCollection<Learning>(
            LearningOrganizerDBSettings.Value.LearningsCollectionName);
    }

    public async Task<List<Category>> GetAsync() =>
        await _categoriesCollection.Find(_ => true).ToListAsync();


    public async Task<Category?> GetAsync(string id) =>
        await _categoriesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<List<Learning>> GetLearningsByCategoryAsync(string categoryName) =>
        await _learningsCollection.Find(x => x.Category == categoryName).ToListAsync();

    public async Task CreateAsync(Category newCategory) =>
        await _categoriesCollection.InsertOneAsync(newCategory);

    public async Task UpdateAsync(string id, Category updateCategory) =>
        await _categoriesCollection.ReplaceOneAsync(x => x.Id == id, updateCategory);

    public async Task RemoveAsync(string id) =>
        // FilterDefinition<Category> filter = Builders<Category>.Filter.Eq(x => x.Id, id);
        await _categoriesCollection.DeleteOneAsync(x => x.Id == id);

    public async Task RemoveAllAsync() =>
        await _categoriesCollection.DeleteManyAsync(_ => true);
    
    // public async Task AddNoteToLearningAsync(string id, Note newNote)
    // {
    //     FilterDefinition<Category> filter = Builders<Category>.Filter.Eq(x => x.Id, id);
    //     UpdateDefinition<Category> update = Builders<Category>.Update.AddToSet<string>(x => x.Notes, updateCategory.Notes);
    //     await _categoriesCollection.UpdateOneAsync(filter, update);
        
    //     // var category = await GetAsync(id);
    //     // if (category is null)
    //     // {
    //     //     return;
    //     // }
    //     // learning.Notes.Add(newNote);
    //     // await UpdateAsync(id, learning);
    // }
}