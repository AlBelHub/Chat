
using chat_backend.Hub;
using Chat.DB;

namespace chat_backend;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSignalR();
        
        builder.Services.AddDbContext<ChatContext>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseCors(opt =>
        {
            opt.WithOrigins("http://localhost:5173")
                .WithMethods("GET", "POST")
                .WithHeaders("authorization", "accept", "content-type", "origin");
        });
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapHub<ChatHub>("/hub");
        
        app.MapControllers();

        app.Run();
    }
}
