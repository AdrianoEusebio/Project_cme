using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Tratament
{

    [Key]
    [Column("id_tratament")]
    public int id_tratament { get; set; }
    
    [Column("washing")]
    public bool washing { get; set; }

    [ForeignKey("material")]
    public Materials? material { get; set; }

    [Column("received_date")]
    public DateTime received_date { get; set; }

    [Column("start_date")]
    public DateTime finish_date { get; set; }

    [Column("finish_date")]
    public bool distrinbution { get; set; }
}