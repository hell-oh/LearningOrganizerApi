namespace LearningOrganizerApi.Models;

public class LearningOrganizerDBSettings
{
    public string ConnectionString { get; set; } = null!;
    public string ConnectionLocal { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string LearningsCollectionName { get; set; } = null!;

    public string UsersCollectionName { get; set; } = null!;

    public string CategoriesCollectionName { get; set; } = null!;

    public string NotesCollectionName { get; set; } = null!;

    public string JWTKey { get; set; } = null!;
}