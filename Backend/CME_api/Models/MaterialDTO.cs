public class MaterialDto
{
    public int IdUser { get; set; }
    public DateTime ExpirationDate { get; set; }
    public MaterialType Type { get; set; }
    public string Name { get; set; } = string.Empty;
}
