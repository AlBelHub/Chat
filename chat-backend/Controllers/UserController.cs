using Chat.DB;
using Microsoft.AspNetCore.Mvc;
using NanoidDotNet;
using BCrypt.Net;

namespace Chat.Controllers;

[Route("/api/{controller}")]
public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly ChatContext _db;

    public UserController(ILogger<UserController> logger, ChatContext dbContext)
    {
        _logger = logger;
        _db = dbContext;
    }
    
    //C R U D
    [HttpPost("/create/")]
    public async Task<IActionResult> CreateUser(
        string username, string password, string first_name, string last_name, string patronymic)
    {
        var user = new ChatContext.User()
        {
            id = Nanoid.Generate(Nanoid.Alphabets.Digits, 10),
            username = username,
            password = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13),
            first_name = first_name,
            last_name = last_name,
            patronymic = patronymic,
            createdAt = DateTime.UtcNow,
            updatedAt = DateTime.UtcNow,
            
        };
        
        await _db.Users.AddAsync(user);

        await _db.SaveChangesAsync();
        
        return Ok(user);
    }
    
    [HttpPost("/get/")]
    public async Task<IActionResult> GetUsers()
    {
        var users = _db.Users;

        return Ok(users);
    }
}