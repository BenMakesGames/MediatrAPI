namespace Silly.Shared.Fruit;

public static class GetFruit
{
    public record Request();
    public record Response(List<string> Fruit);
}