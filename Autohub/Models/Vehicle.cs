using System.ComponentModel.DataAnnotations;

public class Vehicle
{
    [Key]
    public int Id { get; set; }

    public int CustomerId { get; set; }

    [Required]
    public string VehicleNumber { get; set; } = string.Empty;

    [Required]
    public string Make { get; set; } = string.Empty;

    [Required]
    public string Model { get; set; } = string.Empty;

    public int Year { get; set; }

    public Customer? Customer { get; set; }
}