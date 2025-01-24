using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ProcessHistory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdProcess { get; set; }
    //

    [Required]
    public string? SerialMaterial { get; set; }
    //

    [Required]
    public int IdUser { get; set; }
    //

    [Required]
    public DateTime EntryData { get; set; } = DateTime.UtcNow;
    //

    [Required]
    public int? IdReceiving { get; set; } = 0;
    //

    [Required]
    public int? IdDistribution { get; set; } = 0;
    //

    [Required]
    public int? IdWashing { get; set; } = 0;
    //

    [Required]
    [Column(TypeName = "varchar(50)")]
    public MaterialStatus EnumStatus { get; set; }
}
