using Microsoft.EntityFrameworkCore;

namespace Test_fastendpoints.Entities;

public class User
{
    public Guid Id { get; set; }
    public  string Username { get; set; }
    public  string Password { get; set; }

    public User()
    {

    }

    private User(Guid id, string username, string password)
    {
        Id = id;
        Username = username;
        Password = password;        
    }

    public static User Create(string username, string password)
    {
        return new User(Guid.NewGuid(), username, password);
    }

    public static async Task<User> Read(Guid userId, DbContext context)
    {
        return await context.Set<User>().FindAsync(userId);
    }
}
