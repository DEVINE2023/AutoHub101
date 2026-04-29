using System.ComponentModel.DataAnnotations;

public class Customer
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Phone { get; set; } = string.Empty;

    public string? Email { get; set; }
    public string? Address { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

    public List<Vehicle> Vehicles { get; set; } = new();
}