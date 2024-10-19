namespace OurStory.Services;

public interface IMessages
{
    Task<IEnumerable<TB_Messages>> GetMessagesBetweenUsers(int userId1, int userId2);
    Task<TB_Messages> SendMessageAsync(MessageDTO messageDTO);
    Task<TB_Messages> MarkMessageAsSeenAsync(SeenMessageDTO seenMessageDTO);
    Task<TB_Messages> UpdateMessageAsync(MessageDTO messageDTO);
    Task<bool> DeleteMessageAsync(int messageId);

}
