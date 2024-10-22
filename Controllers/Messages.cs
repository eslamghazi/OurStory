namespace OurStory.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "User, Admin")]

public class MessagesController : ControllerBase
{
    private readonly IMessages _messageService;
    private readonly IHubContext<ChatHub> _hubContext;

    public MessagesController(IMessages messageService, IHubContext<ChatHub> hubContext)
    {
        _messageService = messageService;
        _hubContext = hubContext;
    }

    [HttpGet("GetAllMessages/{userId1}/{userId2}")]
    public async Task<IActionResult> GetAllMessages(int userId1, int userId2)
    {
        // Fetch the messages between the two users
        var messages = await _messageService.GetMessagesBetweenUsers(userId1, userId2);

        if (messages == null)
            return BadRequest(new { Message = "لا يوجد رسائل او انه حدث خطأ ما!" });

        // Convert messages to DTOs (if needed) to send them via SignalR
        var messageDtos = messages.Select(m => new MessageDTO
        {
            Id = m.Id,
            SenderId = m.ID_Lovers_Sender_TB.Value,
            ReceiverId = m.ID_Lovers_Receiver_TB.Value,
            Text = m.Text,
            DateCreatedAt = m.DateCreatedAt,
            IsMessageDeleted = m.IsMessageDeleted,
            FilesPath = m.TB_FilesPath
        }).ToList();

        // Send the message history to both users in real-time via SignalR
        await _hubContext.Clients.Users(userId1.ToString(), userId2.ToString())
            .SendAsync("ReceiveMessages", messageDtos);

        // Return the messages as the HTTP response as well (optional)
        return Ok(new { status = 200, Data = messageDtos });
    }

    [HttpPost("SendMessage")]
    public async Task<IActionResult> SendMessage([FromForm] MessageDTO messageDto)
    {

        var message = await _messageService.SendMessageAsync(messageDto);

        if (message == null)
            return BadRequest(new { Message = "حدث خطأ اثناء ارسال الرساله!" });

        await _hubContext.Clients.User(messageDto.ReceiverId.ToString())
            .SendAsync("ReceiveMessage", messageDto);

        return Ok(new { status = 200, Data = message });
    }

    // New: Mark a message as seen
    [HttpPost("MarkMessageAsSeen")]
    public async Task<IActionResult> MarkMessageAsSeen(SeenMessageDTO seenMessageDTO)
    {
        var result = await _messageService.MarkMessageAsSeenAsync(seenMessageDTO);
        if (result == null)
        {
            return BadRequest(new { Message = "لا توجد رساله انو انه حدث خطأ ما!" });

        }

        // Notify the sender that the message has been seen using SignalR
        await _hubContext.Clients.User(result.ID_Lovers_Sender_TB.ToString())
            .SendAsync("MessageSeen", new { seenMessageDTO.messageId, result.SeenAt });

        return Ok(new { status = 200, Data = new { Message = "Message marked as seen.", SeenAt = result.SeenAt } });

    }

    [HttpPost("UpdateMessage")]
    public async Task<IActionResult> UpdateMessage([FromBody] MessageDTO messageDTO)
    {
        var updatedMessage = await _messageService.UpdateMessageAsync(messageDTO);
        if (updatedMessage == null)
        {
            return BadRequest(new { Message = "لا توجد رساله انو انه حدث خطأ ما!" });
        }

        return Ok(new { status = 200, Data = updatedMessage });
    }

    [HttpDelete("DeleteMessage/{messageId}")]
    public async Task<IActionResult> DeleteMessage(int messageId)
    {
        var success = await _messageService.DeleteMessageAsync(messageId);
        if (!success)
        {
            return BadRequest(new { Message = "لا توجد رساله انو انه حدث خطأ ما!" });
        }

        return Ok(new { status = 200, Data = "Message Has Been Deleted" });
    }

}
