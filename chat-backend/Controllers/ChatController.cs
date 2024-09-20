using Chat.DB;
using Microsoft.AspNetCore.Mvc;
using NanoidDotNet;
using BCrypt.Net;
using chat_backend.SimpleModel;
using Microsoft.EntityFrameworkCore;

namespace Chat.Controllers;

public class ChatController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly ChatContext _db;
    
    public ChatController(ILogger<UserController> logger, ChatContext dbContext)
    {
        _logger = logger;
        _db = dbContext;
    }

    [HttpGet("[controller]/getChats")]
    public async Task<IActionResult> GetChats()
    {
        return Ok(_db.Chats.ToList());
    }
    
    [HttpGet("[controller]/getUserChats")]
    public async Task<IActionResult> GetUserChats(string userId)
    {
        var user = _db.Users.FirstOrDefault(user => user.id == userId);
    
        if (user == null)
        {
            return BadRequest("Пользователь не найден");
        }

        var chats = _db.Chats.Where(e => e.ownerId == user.id);
        
        return Ok(chats.ToList());
    }

    [HttpGet("[controller]/getChatMsgs")]
    public async Task<IActionResult> GetChatMsgs(string chatID)
    {
        var chat = _db.Chats.Where(chat => chat.id == chatID)
            .Include(e => e.Messages)
            .FirstOrDefault();
        
        if (chat == null)
        {
            return BadRequest("Чат не найден");
        }
        
        return Ok(chat.Messages.ToList());
        
    }
    
    [HttpPost("[controller]/createChat")]
    public async Task<IActionResult> CreateChat(string OwnerId, string chatName)
    {
        var user = _db.Users.FirstOrDefault(e => e.id == OwnerId);

        if (user == null)
        {
            return BadRequest("Пользователь не найден");
        }

        string id = Nanoid.Generate(Nanoid.Alphabets.Digits, 10);

        var chat = new ChatContext.Chat()
        {
            id = id,
            ownerId = user.id,
            ChatName = chatName,
            usersId = new List<string>(),
            Messages = new List<ChatContext.Message>(),
            createdAt = DateTime.UtcNow,
            updatedAt = DateTime.UtcNow
        };

        
        chat.usersId.Add(OwnerId);
        _db.Chats.Add(chat);

        _db.SaveChanges();

        return Ok("Чат создан");
    }

    [HttpPost("[controller]/addMsg")]
    public async Task<IActionResult> AddMsgToChat(string chatId, [FromBody] string msg, string userId)
    {
        var chat = _db.Chats.Include(e => e.Messages).FirstOrDefault(x => x.id == chatId);
        
        if (chat == null)
        {
            return BadRequest("Чат не найден");
        }

        if (chat.Messages == null)
        {
            chat.Messages = new List<ChatContext.Message>();
        }
        
        chat.Messages.Add(new ChatContext.Message()
        {
            id = Nanoid.Generate(Nanoid.Alphabets.Digits, 17),
            userId = userId,
            body = msg,
            createdAt = DateTime.UtcNow,
            updatedAt = DateTime.UtcNow
        });

        _db.SaveChanges();
        
        return Ok();
    }
    [HttpPost("[controller]/removeMsg")]
    public async Task<IActionResult> RemoveMsgFromChat(string chatId, string msgId)
    {
        var chat = _db.Chats.FirstOrDefault(x => x.id == chatId);
        
        if (chat == null)
        {
            return BadRequest("Чат не найден");
        }
        //Сомнительно, но окэй
        chat.Messages.Remove(_db.Messages.FirstOrDefault(x => x.id == msgId));
        _db.SaveChanges();
        
        return Ok();
    }
    
    [HttpPost("[controller]/removeChat")]
    public async Task<IActionResult> DeleteChat(string chatId)
    {
        var chat = _db.Chats.FirstOrDefault(x => x.id == chatId);

        if (chat == null)
        {
            return BadRequest("Чат не найден");
        }

        if (chat.Messages != null)
        {
            chat.Messages.Clear();
        }
        _db.Update(chat);
        _db.Chats.Remove(chat);
        
        _db.SaveChanges();

        return Ok();
    }
    
}