public class UserRegisterDto
{
    public string Username { get; set; } = string.Empty;
    public string HashPassword { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int IdGroup { get; set; }
}
