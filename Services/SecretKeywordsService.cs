namespace OurStory.Services;

public class SecretKeywordsService : ISecretKeywords
{

    private readonly ApplicationDbContext _DbContext;
    public SecretKeywordsService(ApplicationDbContext dbContext)
    {
        _DbContext = dbContext;
    }

    public async Task<IEnumerable<TB_SecretKeywords>> GetAllSecretKeywords()
    {
        var SecretKeywords = await _DbContext.TB_SecretKeywords.ToListAsync();

        return SecretKeywords;

    }

    public async Task<TB_SecretKeywords> AddSecretKeyword(SecretKeywordDTO SecretKeywordDTO)
    {
        TB_SecretKeywords NewSecretKeyword = new TB_SecretKeywords();

        NewSecretKeyword.Title = SecretKeywordDTO.Title;
        NewSecretKeyword.Keyword = SecretKeywordDTO.Keyword;

        await _DbContext.AddAsync(NewSecretKeyword);

        await _DbContext.SaveChangesAsync();

        return NewSecretKeyword;
    }

    public async Task<TB_SecretKeywords> EditSecretKeyword(SecretKeywordDTO SecretKeywordDTO)
    {
        var SecretKeyword = await _DbContext.TB_SecretKeywords.FirstOrDefaultAsync(x => x.Id == SecretKeywordDTO.Id);

        if (SecretKeyword == null)
            throw new Exception($"SecretKeyword with ID {SecretKeywordDTO.Id} not found.");
        if (!string.IsNullOrEmpty(SecretKeywordDTO.Title))
            SecretKeyword.Title = SecretKeywordDTO.Title;

        if (!string.IsNullOrEmpty(SecretKeywordDTO.Keyword))
            SecretKeyword.Keyword = SecretKeywordDTO.Keyword;

        await _DbContext.SaveChangesAsync();

        return SecretKeyword;
    }

    public async Task<TB_SecretKeywords> DeleteSecretKeyword(int id)
    {
        var SecretKeyword = await _DbContext.TB_SecretKeywords.FirstOrDefaultAsync(x => x.Id == id);

        if (SecretKeyword == null)
            throw new Exception($"SecretKeyword with ID {id} not found.");

        _DbContext.Remove(SecretKeyword);

        await _DbContext.SaveChangesAsync();

        return SecretKeyword;
    }
}
