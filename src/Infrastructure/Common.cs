using System.Xml;
using Infrastructure.Interfaces;

namespace Infrastructure;

public static class Common
{
    public static string SerializeEntity<T>(T value)
    {
        if (value == null) throw new ArgumentException("Empty Entity", "Error");

        const string fmt = "<{0}>{1}</{0}>\n";
        var xml = @"<?xml version=""1.0"" ?>" + "\n";
        var type = value.GetType();
        var methodInfos = typeof(T).GetProperties();
        var xmlContent = string.Empty;
        foreach (var methodInfo in methodInfos)
        {
            var methodValue = type.GetProperty(methodInfo.Name).GetValue(value, null);
            if (methodValue is null) continue;
            xmlContent += string.Format(fmt, methodInfo.Name, methodValue);
        }

        Array.Clear(methodInfos, 0, methodInfos.Length);
        xml += string.Format(fmt, type.Name, xmlContent);

        return xml;
    }
    
    public static T DeserializeEntity<T>(string value) where T : class, IEmv
    {
        if (value.Trim().Length == 0) throw new ArgumentException("Empty Entity", "Error");

        var xml = new XmlDocument();
        xml.LoadXml(value);
        var section = xml.DocumentElement;

        var type = typeof(T);
//            if (!type.Name.Equals(section.Name, StringComparison.OrdinalIgnoreCase))
//                throw new ArgumentException("The entity is not the expected", "Error");

        var name = String.Empty;
        if (section is not null && section.HasAttributes)
            name = section.Attributes.GetNamedItem("RequestType").Value;

        var typeName = $"Infrastructure.Entity.{section.Name}{name}, {type.Assembly.FullName}";
        var actualType = Type.GetType(typeName, true);

        var entity = Activator.CreateInstance(actualType);
        var methodInfos = actualType.GetProperties();
        foreach (XmlNode childNode in section.ChildNodes)
        {
            var result = Array.Find(methodInfos,
                s => s.Name.Equals(childNode.Name, StringComparison.OrdinalIgnoreCase));

            result?.SetValue(entity, Convert.ChangeType(childNode.InnerText, result.PropertyType), null);
        }

        xml.RemoveAll();
        Array.Clear(methodInfos, 0, methodInfos.Length);
        return entity as T;
    }

}