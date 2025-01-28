using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Material
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdMaterial { get; set; }
    //

    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;
    //

    [Required]
    [MaxLength(50)]
    public string Serial { get; set; } = string.Empty;
    //

    [Required]
    public DateTime EntryDate { get; set; } = DateTime.UtcNow;
    //

    [Required]
    public DateTime ExpirationDate { get; set; }
    //

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string? Type { get; set; }
    //

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string Status { get; set; } = MaterialStatus.SEM_PROCESSOS.ToString();
    //

    [ForeignKey(nameof(User))]
    public int IdUser { get; set; }
    //

    public User User { get; set; } = null!;
    //
    public void GenerateSerial()
    {
        Serial = $"{Name.Substring(0, 3).ToUpper()}{IdMaterial.ToString("D3")}";
    }
}