using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Receiving
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdReceiving { get; set; }
    //

    [Required]
    public string SerialMaterial { get; set; } = string.Empty;
    //

    [Required]
    public int IdUser { get; set; }
    //

    [Required]
    public DateTime EntryData { get; set; } = DateTime.UtcNow;
}
