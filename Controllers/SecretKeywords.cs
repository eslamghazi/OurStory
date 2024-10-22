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
            throw new Exception("No Secret Keywords");

        return Ok(SecretKeywords);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("AddSecretKeyword")]
    public async Task<IActionResult> AddSecretKeyword(SecretKeywordDTO SecretKeywordDTO)
    {
        var SecretKeyword = await _SecretKeywordsService.AddSecretKeyword(SecretKeywordDTO);

        if (SecretKeyword == null)
            throw new Exception("Cannot Add SecretKeyword");

        return Ok(SecretKeyword);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("EditSecretKeyword")]
    public async Task<IActionResult> EditSecretKeyword(SecretKeywordDTO SecretKeywordDTO)
    {
        var SecretKeyword = await _SecretKeywordsService.EditSecretKeyword(SecretKeywordDTO);

        if (SecretKeyword == null)
            throw new Exception("Cannot Edit SecretKeyword");

        return Ok(SecretKeyword);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("DeleteSecretKeyword")]
    public async Task<IActionResult> DeleteSecretKeyword(int id)
    {
        var SecretKeyword = await _SecretKeywordsService.DeleteSecretKeyword(id);

        if (SecretKeyword == null)
            throw new Exception("Cannot Delete SecretKeyword");

        return Ok(SecretKeyword);
    }
}


