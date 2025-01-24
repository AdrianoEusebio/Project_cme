using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } 
    //

    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = string.Empty;
    //

    [Required]
    public string HashPassword { get; set; } = string.Empty;
    //

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    //

    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    //
    
    [ForeignKey(nameof(UserGroup))]
    public int IdGroup { get; set; }
    //

    public UserGroup UserGroup { get; set; } = null!;
    //

    public string GenerateHashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }

}
