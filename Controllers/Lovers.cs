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

            return BadRequest(new { Message = "اسم المستخدم او كلمة السر خطأ" });
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

        return Ok(new { StatusCode = 200, Data = loggedUserInfo });
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

        if (Lovers == null)
            return BadRequest(new { Message = "لا يوجد مستخدمين او انه حدث خطأ ما!" });

        return Ok(new { StatusCode = 200, Data = Lovers });
    }

    [HttpGet("GetAllDescriptions/{loverId}")]
    public async Task<IActionResult> GetAllDescriptions(int loverId)
    {

        var Descriptions = await _LoverService.getAllDescriptions(loverId);

        if (Descriptions == null)
            return BadRequest(new { Message = "لا يوجد وصف او انه حدث خطأ ما!" });

        return Ok(new { StatusCode = 200, Data = Descriptions });
    }

    [HttpPost("UpdateDescription")]
    public async Task<IActionResult> UpdateDescription([FromForm] DescriptionsDTO DTO)
    {

        var Description = await _LoverService.UpdateDescription(DTO);

        if (Description == null)
            return BadRequest(new { Message = $"لا يمكن التعديل {DTO.Id} او انه حدث خطأ ما!" });

        return Ok(new { StatusCode = 200, Data = Description });
    }

    [HttpDelete("DeleteDescription/{id}")]
    public async Task<IActionResult> DeleteDescription(int id)
    {

        var Description = await _LoverService.DeleteDescription(id);

        if (Description == null)
            return BadRequest(new { Message = $"لا يمكن حذف {id} او انه حدث خطأ ما!" });

        return Ok(new { StatusCode = 200, Data = Description });
    }

    [HttpPost("UpdateLover")]
    public async Task<IActionResult> UpdateLover([FromForm] UpdateLoverDTO DTO, bool IsEditDescription = false)
    {

        var Lover = await _LoverService.UpdateLover(DTO, IsEditDescription);

        if (Lover == null)
            return BadRequest(new { Message = $"لا يمكن التعديل {DTO.Name} او انه حدث خطأ ما!" });

        return Ok(new { StatusCode = 200, Data = Lover });
    }

}
