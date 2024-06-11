using FastEndpoints;
using Test_fastendpoints.Features.User.GetUser;
using System.Threading;
using System.Threading.Tasks;
using Test_fastendpoints.Infrastructure.Data;

namespace Test_fastendpoints.Features.User.GetUser
{
    public class Endpoint(ApplicationDbContext _dbContext) : Endpoint<EmptyRequest, Model.Response>
    {

        public override void Configure()
        {
            Get("/api/User/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
        {
            Guid userId = Route<Guid>("id");
            // Create a new User instance
            var user = await _dbContext.Users.FindAsync(new object[] { userId }, ct);

            if (user == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            // Return a response
            await SendAsync(new Model.Response(user.Username, user.Password));

         }
    }
}

