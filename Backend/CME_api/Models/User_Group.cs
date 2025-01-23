using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User_Group
{
    [Key]
    [Column("id_group")]
    public int id_group { get; set; }

    [Column("name")]
    public string? name { get; set; }
    [Column("description")]
    public string? description { get; set; }
    [Column("nivel")]
    public int nivel { get; set; }
}