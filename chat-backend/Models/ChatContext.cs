using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Chat.DB;

public class ChatContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Database=mydb2;Username=efcore2;Password=efcore2");

    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key,Column(Order = 0)]
        public string userID { get; set; }
        
        public string id { get; set; } 
        public string username { get; set; }
        public string password { get; set; }
        
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string patronymic { get; set; }

        
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        
    }

    public class Chat
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key,Column(Order = 0)]
        public string chatId { get; set; } = null!;
        public string id { get; set; } = null!;
        public string ownerId { get; set; } = null!;
        public string ChatName { get; set; } = null!;
        public List<string>? usersId { get; set; }

        // public List<User> Users { get; set; }
        public List<Message> Messages { get; set; }
        
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }

    public class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key,Column(Order = 0)]
        public string messageId { get; set; }
        
        public string id { get; set; }
        public string userId { get; set; }
        
        public string body { get; set; }
        
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}