using MediatR;

namespace Silly.Application.Fruit;

public sealed class GetFruit: IRequestHandler<GetFruit.Request, GetFruit.Response>
{
    public sealed record Request(): IRequest<Response>;
    public sealed record Response(List<string> Fruit);

    public async Task<Response> Handle(Request request, CancellationToken ct)
    {
        return new(Repository.Repository.Fruit);
    }
}