namespace OurStory.Services;

public interface IBlogs
{
    Task<IEnumerable<TB_Blogs>> GetAll(int blogType = 0, int events = 0, int published = 0, int lover = 0);
    Task<TB_Blogs> GetById(int id);
    Task<TB_Blogs> AddAsync(AddUpdateBlogsDTO blogDTO);
    Task<TB_Blogs> UpdateAsync(AddUpdateBlogsDTO blogDTO);
    Task<TB_Blogs> DeleteAsync(int id);
    Task<TB_FilePaths> DeleteFile(int id);
    Task<TB_Blogs> AddCommentBlogAsync(CommentBlogDTO CommentBlogDTO);
    Task<TB_Blogs> UpdateCommentBlogAsync(CommentBlogDTO CommentBlogDTO);
    Task<TB_Blogs> UpdateLikeBlogAsync(LikeBlogDTO LikeBlogDTO);
    Task<TB_Blogs> DeleteCommentBlogAsync(CommentBlogDTO CommentBlogDTO);

}
