namespace OurStory.Helpers;

public class ChatHub : Hub
{
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
}
