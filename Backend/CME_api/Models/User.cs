
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User{
    [Key]
    [Column("dataAtual")]
    public int id_user { get; set; }

    [Column("name")]
    public string? name { get; set; }
    [Column("email")]
    public string? email { get; set; }

    [ForeignKey("group")]
    public User_Group? group { get; set; }
    [Column("hashPassword")]
    public string? hashPassword { get; set; }
    [Column("id_user")]
    
    public DateTime dataAtual { get; set; }
}