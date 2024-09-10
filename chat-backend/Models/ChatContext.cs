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
        public int userID { get; set; }
        
        
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
        public int chatId { get; set; }

        public List<User> Users { get; set; }
        public List<Message> Messages { get; set; }
        
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }

    public class Message
    {
        public int messageId { get; set; }
        public int userId { get; set; }
        
        public string body { get; set; }
        
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}