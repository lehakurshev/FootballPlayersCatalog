
namespace Domain;

public class FootballPlayer
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    
    public string? Paul { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? TeamName { get; set; }
    public string? Country { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? EditDate { get; set; }
}