using System.Reflection;
using MediatR;
using Newtonsoft.Json.Serialization;

namespace Mediatr.Web;

public class KnownTypesBinder: ISerializationBinder
{
    private List<Type> KnownTypes { get; }

    public KnownTypesBinder(Assembly assembly)
    {
        var request = typeof(IRequest<>); // IRequest, from MediatR

        KnownTypes = assembly
            .GetTypes()
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == request
            ))
            .ToList();
    }

    public Type BindToType(string? assemblyName, string typeName)
    {
        return KnownTypes.FirstOrDefault(t => t.FullName == typeName);
    }

    public void BindToName(Type serializedType, out string? assemblyName, out string? typeName)
    {
        assemblyName = null;
        typeName = serializedType.FullName;
    }
}