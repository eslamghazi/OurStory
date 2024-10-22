namespace OurStory.Services;

public interface ISecretKeywords
{
    Task<IEnumerable<TB_SecretKeywords>> GetAllSecretKeywords();
    Task<TB_SecretKeywords> AddSecretKeyword(SecretKeywordDTO SecretKeywordDTO);
    Task<TB_SecretKeywords> EditSecretKeyword(SecretKeywordDTO SecretKeywordDTO);
    Task<TB_SecretKeywords> DeleteSecretKeyword(int id);

}
