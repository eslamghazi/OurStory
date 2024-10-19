namespace OurStory.Helpers;

public class ChatHub : Hub
{
    private readonly ApplicationDbContext _context;

    public ChatHub(ApplicationDbContext context)
    {
        _context = context;
    }

    // Existing method to send a single message
    public async Task SendMessage(MessageDTO messageDTO)
    {
        // Send the new message to the receiver in real-time
        await Clients.User(messageDTO.ReceiverId.ToString()).SendAsync("ReceiveMessage", messageDTO);
    }

    // New method to get messages between users in real-time
    public async Task GetMessages(int userId1, int userId2, List<MessageDTO> messages)
    {
        // Send the retrieved message history to both users
        await Clients.Users(userId1.ToString(), userId2.ToString()).SendAsync("ReceiveMessages", messages);
    }

    // New method for handling message read/seen
    public async Task MarkMessageAsSeen(int messageId, int userId)
    {
        var message = await _context.TB_Messages.FindAsync(messageId);
        if (message != null && message.ID_Lovers_Receiver_TB == userId && !message.IsSeen.Value)
        {
            message.IsSeen = true;
            message.SeenAt = DateTime.Now;

            await _context.SaveChangesAsync();

            // Notify the sender that the message has been seen
            await Clients.User(message.ID_Lovers_Sender_TB.ToString())
                .SendAsync("MessageSeen", new { messageId = messageId, seenAt = message.SeenAt });
        }

    }
}