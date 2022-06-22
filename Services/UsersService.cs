using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LearningOrganizerApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace LearningOrganizerApi.Services;

public class UsersService
{
    private readonly IMongoCollection<User> _usersCollection;

    private readonly string _key;

    public UsersService(
        IOptions<LearningOrganizerDBSettings> LearningOrganizerDBSettings)
    {
        var mongoClient = new MongoClient(
            LearningOrganizerDBSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            LearningOrganizerDBSettings.Value.DatabaseName);

        _usersCollection = mongoDatabase.GetCollection<User>(
            LearningOrganizerDBSettings.Value.UsersCollectionName);

        _key = LearningOrganizerDBSettings.Value.JWTKey;
    }

    public async Task<List<User>> GetAsync() =>
        await _usersCollection.Find(_ => true).ToListAsync();

    public async Task<User?> GetAsync(string id) =>
        await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(User newUser) =>
        await _usersCollection.InsertOneAsync(newUser);

    public async Task UpdateAsync(string id, User updatedUser) =>
        await _usersCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

    public async Task RemoveAsync(string id) =>
        // FilterDefinition<User> filter = Builders<User>.Filter.Eq(x => x.Id, id);
        await _usersCollection.DeleteOneAsync(x => x.Id == id);

    public async Task RemoveAllAsync() =>
        await _usersCollection.DeleteManyAsync(_ => true);
    
    // public async Task AddNoteToUserAsync(string id, Note newNote)
    // {
    //     FilterDefinition<User> filter = Builders<User>.Filter.Eq(x => x.Id, id);
    //     UpdateDefinition<User> update = Builders<User>.Update.AddToSet<string>(x => x.Notes, updatedUser.Notes);
    //     await _usersCollection.UpdateOneAsync(filter, update);
        
    //     // var user = await GetAsync(id);
    //     // if (user is null)
    //     // {
    //     //     return;
    //     // }
    //     // user.Notes.Add(newNote);
    //     // await UpdateAsync(id, user);
    // }


    public string Authenticate(string email, string password)
    {
        var user = _usersCollection.Find(x => x.Email == email && x.Password == password).FirstOrDefault();
        if (user is null)
        {
            return null;
        }
    
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var tokenKey = Encoding.ASCII.GetBytes(_key);

        var tokenDescriptor = new SecurityTokenDescriptor(){

            Subject = new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.Email, email)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

}