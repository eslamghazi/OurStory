namespace OurStory.Controllers;
[ApiController]
[Route("[controller]")]
public class SecretKeywords : ControllerBase
{
    private readonly ISecretKeywords _SecretKeywordsService;

    public SecretKeywords(ISecretKeywords SecretKeywordsService)
    {
        _SecretKeywordsService = SecretKeywordsService;
    }

    [AllowAnonymous]
    [HttpGet("GetAllSecretKeywords")]
    public async Task<IActionResult> GetAllSecretKeywords()
    {
        var SecretKeywords = await _SecretKeywordsService.GetAllSecretKeywords();

        if (!SecretKeywords.Any())
            return BadRequest(new { Message = "لا توجد secret keywords انو انه حدث خطأ ما!" });

        return Ok(new { StatusCode = 200, Data = SecretKeywords });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("AddSecretKeyword")]
    public async Task<IActionResult> AddSecretKeyword(SecretKeywordDTO SecretKeywordDTO)
    {
        var SecretKeyword = await _SecretKeywordsService.AddSecretKeyword(SecretKeywordDTO);

        if (SecretKeyword == null)
            return BadRequest(new { Message = "لا يمكن اضافة secret keyword!" });

        return Ok(new { StatusCode = 200, Data = SecretKeyword });

    }

    [Authorize(Roles = "Admin")]
    [HttpPost("EditSecretKeyword")]
    public async Task<IActionResult> EditSecretKeyword(SecretKeywordDTO SecretKeywordDTO)
    {
        var SecretKeyword = await _SecretKeywordsService.EditSecretKeyword(SecretKeywordDTO);

        if (SecretKeyword == null)
            return BadRequest(new { Message = "لا يمكن التعديل علي secret keyword!" });

        return Ok(new { StatusCode = 200, Data = SecretKeyword });

    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("DeleteSecretKeyword")]
    public async Task<IActionResult> DeleteSecretKeyword(int id)
    {
        var SecretKeyword = await _SecretKeywordsService.DeleteSecretKeyword(id);

        if (SecretKeyword == null)
            return BadRequest(new { Message = "لا يمكن حذف secret keyword!" });

        return Ok(new { StatusCode = 200, Data = SecretKeyword });

    }
}


