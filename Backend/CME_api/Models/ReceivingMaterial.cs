using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ReceivingMaterial
{
    [Key]
    [Column("id_receiving_material")]
    public int id_receiving_material { get; set; }

    [ForeignKey("material")]
    public Materials? material { get; set; }
    [Column("quantity")]
    public int quantity { get; set; }
    [Column("entry_date")]
    public DateTime entry_date { get; set; }
    [Column("need_tratament")]
    public Boolean need_tratament { get; set; }
}