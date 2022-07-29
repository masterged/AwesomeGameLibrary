namespace AwesomeGameLibrary.Domain.Database.Entities;

public class Game
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string OwnerName { get; set; }

    public int GenreId { get; set; }
    public Genre Genre { get; set; }
    
    public int PlatformId { get; set; }
    public Platform Platform { get; set; }
}