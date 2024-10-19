namespace OurStory.DTOs;

public class LoginDTO
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoggedUserDTO
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string? Role { get; set; }
    public string? Token { get; set; }

}
