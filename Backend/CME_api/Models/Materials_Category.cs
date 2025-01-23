using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Materials_Category
{
    [Key]
    [Column("id_category")]
    public int id_category { get; set; }
    [Column("name")]
    public string? name { get; set; }
}