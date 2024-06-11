namespace Test_fastendpoints.Features.User.CreateUser;

public class Model
{
    public record Request(string username, string password);
    public record Response(string username, string password);
}
