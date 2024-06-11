using FastEndpoints;
using Test_fastendpoints.Features.User.CreateUser;
using System.Threading;
using System.Threading.Tasks;
using Test_fastendpoints.Infrastructure.Data;

namespace Test_fastendpoints.Features.User.CreateUser
{
    public class Endpoint(ApplicationDbContext _dbContext) : Endpoint<Model.Request, Model.Response>
    {

        public override void Configure()
        {
            Post("/api/CreateUser");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Model.Request req, CancellationToken ct)
        {
            // Create a new User instance
            var newUser = Entities.User.Create(req.username, req.password);

            // Add the new instance to the DbContext
            _dbContext.Users.Add(newUser);

            // Save the changes to the database
            await _dbContext.SaveChangesAsync(ct);

            // Return a response
            await SendAsync(new Model.Response(newUser.Username, newUser.Password));
        }
    }
}

