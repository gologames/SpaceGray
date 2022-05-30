using System.Reflection;

namespace SpaceGray.Test.Util;

public static class ObjectExtensions
{
    public static T GetField<T>(this object obj, string fieldName)
    {
        var field = obj.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        if (field == null) throw new MissingFieldException();
        return (T)field.GetValue(obj)!;
    }
    public static void SetField(this object obj, string fieldName, object value)
    {
        var field = obj.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        if (field == null) throw new MissingMemberException();
        field.SetValue(obj, value);
    }

    public static void SetProperty(this object obj, string propertyName, object value)
    {
        var property = obj.GetType().GetProperty(propertyName);
        if (property == null) throw new MissingMemberException();
        property.SetValue(obj, value);
    }

    private static object? InvokeImplementation(this object obj, string methodName, params object[] parameters)
    {
        var method = obj.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        if (method == null) throw new MissingMethodException();
        return method.Invoke(obj, parameters);
    }
    public static void Invoke(this object obj, string methodName, params object[] parameters) =>
        InvokeImplementation(obj, methodName, parameters);
    public static T Invoke<T>(this object obj, string methodName, params object[] parameters) =>
        (T)InvokeImplementation(obj, methodName, parameters)!;
}
