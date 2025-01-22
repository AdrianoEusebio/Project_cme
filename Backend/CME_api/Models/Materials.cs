public class Materials
{
    private int id_material { get; set; }
    private string? name { get; set; }
    private string? type { get; set; }
    private string? serial_number { get; set; }
    private Materials_Category? category { get; set; }
    private DateTime dataAtual { get; set; }
}