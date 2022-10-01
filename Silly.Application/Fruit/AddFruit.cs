using MediatR;

namespace Silly.Application.Fruit;

public sealed class AddFruit: IRequestHandler<AddFruit.Request, AddFruit.Response>
{
    public sealed record Request(string Name): IRequest<Response>;
    public sealed record Response(bool Success);

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        var fruit = request.Name.Trim();

        if (Repository.Repository.Fruit.Contains(fruit, StringComparer.OrdinalIgnoreCase))
            return new(false);

        Repository.Repository.Fruit.Add(fruit);

        return new(true);
    }
}