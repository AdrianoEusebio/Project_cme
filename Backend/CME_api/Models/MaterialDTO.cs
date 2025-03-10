using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

public class MaterialDto
{
    public int IdUser { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string? Type { get; set; }
    public string Name { get; set; } = string.Empty;
}
