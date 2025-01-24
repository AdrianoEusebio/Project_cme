using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Washing
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdWashing { get; set; }
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
    public bool IsWashed { get; set; } = false;
}

