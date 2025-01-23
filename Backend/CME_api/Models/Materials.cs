using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Materials
{
    [Key]
    [Column("id_material")]
    public int id_material { get; set; }
    [Column("name")]
    public string? name { get; set; }
    [Column("type")]
    public string? type { get; set; }
    [Column("serial_number")]
    public string? serial_number { get; set; }
    [Column("category")]
    public Materials_Category? category { get; set; }
    [Column("description")]
    public DateTime dataAtual { get; set; }
}