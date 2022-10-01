using MediatR;
using Silly.Database;

namespace Silly.Application.Fruit;

public sealed class GetFruit: IRequestHandler<GetFruit.Request, GetFruit.Response>
{
    public sealed record Request(): IRequest<Response>;
    public sealed record Response(List<string> Fruit);

    private Repository Repository { get; }

    public GetFruit(Repository repository)
    {
        Repository = repository;
    }
    
    public async Task<Response> Handle(Request request, CancellationToken ct)
    {
        return new(Repository.Fruit.ToList());
    }
}