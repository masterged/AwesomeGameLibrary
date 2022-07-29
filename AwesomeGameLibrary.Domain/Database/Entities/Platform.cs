namespace AwesomeGameLibrary.Domain.Database.Entities;

public class Platform
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Game> Games { get; set; }
}