using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Distribution
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdDistribution { get; set; }
    //

    [Required]
    public string SerialMaterial { get; set; } = string.Empty;
    //

    [Required]
    public int IdUser { get; set; }
    //

    [Required]
    public DateTime EntryDate { get; set; } = DateTime.UtcNow;
    //

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string? Sector { get; set; } 
}