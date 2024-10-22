namespace OurStory.Controllers;
[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Admin")]
public class SecretKeywords : ControllerBase
{
    private readonly ISecretKeywords _SecretKeywordsService;

    public SecretKeywords(ISecretKeywords SecretKeywordsService)
    {
        SecretKeywordsService = _SecretKeywordsService;
    }

    [Authorize(Roles = "User, Admin")]
    [HttpGet("GetAllSecretKeywords")]
    public async Task<IActionResult> GetAllSecretKeywords()
    {
        var SecretKeywords = await _SecretKeywordsService.GetAllSecretKeywords();

        if (!SecretKeywords.Any())
            throw new Exception("No Secret Keywords");

        return Ok(SecretKeywords);
    }

    [HttpPost("AddSecretKeyword")]
    public async Task<IActionResult> AddSecretKeyword(SecretKeywordDTO SecretKeywordDTO)
    {
        var SecretKeyword = await _SecretKeywordsService.AddSecretKeyword(SecretKeywordDTO);

        if (SecretKeyword == null)
            throw new Exception("Cannot Add SecretKeyword");

        return Ok(SecretKeyword);
    }

    [HttpPost("EditSecretKeyword")]
    public async Task<IActionResult> EditSecretKeyword(SecretKeywordDTO SecretKeywordDTO)
    {
        var SecretKeyword = await _SecretKeywordsService.EditSecretKeyword(SecretKeywordDTO);

        if (SecretKeyword == null)
            throw new Exception("Cannot Edit SecretKeyword");

        return Ok(SecretKeyword);
    }

    [HttpDelete("DeleteSecretKeyword")]
    public async Task<IActionResult> DeleteSecretKeyword(int id)
    {
        var SecretKeyword = await _SecretKeywordsService.DeleteSecretKeyword(id);

        if (SecretKeyword == null)
            throw new Exception("Cannot Delete SecretKeyword");

        return Ok(SecretKeyword);
    }
}


