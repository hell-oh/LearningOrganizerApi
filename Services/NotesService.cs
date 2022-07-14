using LearningOrganizerApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LearningOrganizerApi.Services;

public class NotesService
{
    private readonly IMongoCollection<Note> _notesCollection;

    public NotesService(
        IOptions<LearningOrganizerDBSettings> LearningOrganizerDBSettings)
    {
        var mongoClient = new MongoClient(
            LearningOrganizerDBSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            LearningOrganizerDBSettings.Value.DatabaseName);

        _notesCollection = mongoDatabase.GetCollection<Note>(
            LearningOrganizerDBSettings.Value.NotesCollectionName);
    }

    public async Task<List<Note>> GetAsync() =>
        await _notesCollection.Find(x => x.Public == true).ToListAsync();


    //Metodo de prueba, es más eficiente obtener desde el mismo usuario a menos que se quiera obtener datos adicionales de todos los notes del usuario
    public async Task<List<Note>> GetByUserAsync(string id) =>
        await _notesCollection.Find(x => x.UserId == id).ToListAsync();

    public async Task<Note?> GetAsync(string id) =>
        await _notesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Note newNote) =>
        await _notesCollection.InsertOneAsync(newNote);

    public async Task UpdateAsync(string id, Note updatedNote) =>
        await _notesCollection.ReplaceOneAsync(x => x.Id == id, updatedNote);

    public async Task RemoveAsync(string id) =>
        // FilterDefinition<Note> filter = Builders<Note>.Filter.Eq(x => x.Id, id);
        await _notesCollection.DeleteOneAsync(x => x.Id == id);

    public async Task RemoveAllAsync() =>
        await _notesCollection.DeleteManyAsync(_ => true);
    
    // public async Task AddNoteToNoteAsync(string id, Note newNote)
    // {
    //     FilterDefinition<Note> filter = Builders<Note>.Filter.Eq(x => x.Id, id);
    //     UpdateDefinition<Note> update = Builders<Note>.Update.AddToSet<string>(x => x.Notes, updatedNote.Notes);
    //     await _notesCollection.UpdateOneAsync(filter, update);
        
    //     // var note = await GetAsync(id);
    //     // if (note is null)
    //     // {
    //     //     return;
    //     // }
    //     // note.Notes.Add(newNote);
    //     // await UpdateAsync(id, note);
    // }
}