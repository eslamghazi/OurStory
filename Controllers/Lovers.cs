namespace OurStory.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize(Roles = "User, Admin")]

public class Lovers : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly JwtHelper _jwtHelper;
    private readonly ILovers _LoverService;

    public Lovers(ApplicationDbContext dbContext, JwtHelper jwtHelper, ILovers loverService)
    {
        _dbContext = dbContext;
        _jwtHelper = jwtHelper;
        _LoverService = loverService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginRequest)
    {
        var user = await _dbContext.TB_Lovers
            .Include(x => x.TB_FilesPath_ProfilePicture)
            .FirstOrDefaultAsync(u => u.Name == loginRequest.Username);

        if (user == null || !VerifyPassword(loginRequest.Password, user.Password))
        {
            return Unauthorized("Invalid username or password");
        }

        var token = _jwtHelper.GenerateJwtToken(user);

        var loggedUserInfo = new LoggedUserDTO
        {
            Id = user.Id,
            Username = user.Name,
            Password = user.Password,
            Role = user.Role,
            Token = token,
            ProfilePicture = user.TB_FilesPath_ProfilePicture
        };

        return Ok(loggedUserInfo);
    }

    private bool VerifyPassword(string password, string storedHash)
    {
        // For simplicity, use plaintext password. In production, you should hash passwords.
        return password == storedHash;
    }

    [HttpGet("GetAllLovers")]
    public async Task<IActionResult> GetAllLovers()
    {

        var Lovers = await _LoverService.GetAllLovers();


        return Ok(Lovers);
    }

    [HttpGet("GetAllDescriptions/{loverId}")]
    public async Task<IActionResult> GetAllDescriptions(int loverId)
    {

        var Descriptions = await _LoverService.getAllDescriptions(loverId);


        return Ok(Descriptions);
    }

    [HttpPost("UpdateDescription")]
    public async Task<IActionResult> UpdateDescription([FromForm] DescriptionsDTO DTO)
    {

        var Description = await _LoverService.UpdateDescription(DTO);

        if (Description == null)
            throw new Exception($"لا يمكن التعديل {DTO.Id}");

        return Ok(Description);
    }

    [HttpDelete("DeleteDescription/{id}")]
    public async Task<IActionResult> DeleteDescription(int id)
    {

        var Description = await _LoverService.DeleteDescription(id);

        if (Description == null)
            throw new Exception($"لا يمكن حذف {id}");

        return Ok(Description);
    }

    [HttpPost("UpdateLover")]
    public async Task<IActionResult> UpdateLover([FromForm] UpdateLoverDTO DTO, bool IsEditDescription = false)
    {

        var Lover = await _LoverService.UpdateLover(DTO, IsEditDescription);

        if (Lover == null)
            throw new Exception($"لا يمكن التعديل {DTO.Name}");

        return Ok(Lover);
    }

}
