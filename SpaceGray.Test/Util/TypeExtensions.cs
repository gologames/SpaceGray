using System.Reflection;

namespace SpaceGray.Test.Util;

public static class TypeExtensions
{
    private static object? InvokeStaticImplementation(this Type type, string methodName, params object[] parameters)
    {
        var method = type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
        if (method == null) throw new MissingMethodException();
        return method.Invoke(null, parameters);
    }

    public static void InvokeStatic(this Type type, string methodName, params object[] parameters) =>
        InvokeStaticImplementation(type, methodName, parameters);

    public static T InvokeStatic<T>(this Type type, string methodName, params object[] parameters) =>
        (T)InvokeStaticImplementation(type, methodName, parameters)!;
}
