using Chat.DB;
using Microsoft.AspNetCore.Mvc;
using NanoidDotNet;
using BCrypt.Net;
using chat_backend.SimpleModel;
using Microsoft.EntityFrameworkCore;

namespace Chat.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly ChatContext _db;

    public UserController(ILogger<UserController> logger, ChatContext dbContext)
    {
        _logger = logger;
        _db = dbContext;
    }
    
    [HttpPost("[controller]/create")]
    public async Task<IActionResult> CreateUser(SimpleUser newUser)
    {
        var username = _db.Users.FirstOrDefault(x => x.username == newUser.username);
        
        if (username != null)
        {
            return BadRequest("Пользователь с таким именем уже занят");
        }
        
        var user = new ChatContext.User()
        {
            id = Nanoid.Generate(Nanoid.Alphabets.Digits, 10),
            username = newUser.username,
            password = BCrypt.Net.BCrypt.EnhancedHashPassword(newUser.password, 13),
            first_name = newUser.password,
            last_name = newUser.last_name,
            patronymic = newUser.patronymic,
            createdAt = DateTime.UtcNow,
            updatedAt = DateTime.UtcNow,
        };
        
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
        
        return Ok(user);
    }
    
    [HttpGet("[controller]/get")]
    public async Task<IActionResult> GetUsers()
    {
        var users = _db.Users;

        return Ok(users);
    }

    [HttpGet("[controller]/get/{id}")]
    public async Task<IActionResult> GetUserByID(string id)
    {
        var user = await _db.Users.FirstOrDefaultAsync(x => x.id == id);

        if (user == null)
        {
            return BadRequest("Пользователь не найден");
        }
        
        return Ok(user);
    }
    [HttpPost("[controller]/update")]
    public async Task<IActionResult> UpdateUser(SimpleUser newUser)
    {
        var user = _db.Users.FirstOrDefault(x => x.id == newUser.id);
        
        if (user == null)
        {
            return BadRequest("Пользователь не найден");
        }

        var username = _db.Users.FirstOrDefault(x => x.username == newUser.username);
        
        if (username != null)
        {
            return BadRequest("Пользователь с таким именем уже занят");
        }
        
        user.username = newUser.username;
        user.first_name = newUser.first_name;
        user.last_name = newUser.last_name;
        user.patronymic = newUser.patronymic;
        user.updatedAt = DateTime.UtcNow;

        _db.Entry(user).State = EntityState.Modified;
        _db.SaveChanges();
        
        return Ok(user);
    }

    [HttpDelete("[controller]/remove/{id}")]
    public async Task<IActionResult> RemoveUser(string id)
    {
        var user = _db.Users.FirstOrDefault(x => x.id == id);

        if (user == null)
        {
            return BadRequest("Пользователь не найден");
        }

        _db.Remove(user);

        return Ok("Пользователь удалён");
    }
}