using System.Xml;
using System.Xml.Serialization;

namespace SB.Infrastructure;

public static class Common
{
    public static T DeserializarXml<T>(string xml)
    {
        if (xml.Trim().Length == 0) throw new ArgumentException("Empty Entity", "Error");
        
        var xmlEntity = new XmlDocument();
        xmlEntity.LoadXml(xml);
        var section = xmlEntity.DocumentElement;
        var type = typeof(T);
        var typeName = $"SB.Infrastructure.Entity.{section.Name}, {type.Assembly.FullName}";
        var actualType = Type.GetType(typeName, true);
        xmlEntity.RemoveAll();
        
        var serializer = new XmlSerializer(actualType);
        using var reader = new StringReader(xml);
        return (T)serializer.Deserialize(reader)!;
    }
    
    public static string SerializarXml<T>(T obj)
    {
        var serializer = new XmlSerializer(typeof(T));
        using var writer = new StringWriter();
        serializer.Serialize(writer, obj);
        return writer.ToString();
    }
    
    public static string ToApplicationPath(string fileName)
    {
        var exePath = Path.GetDirectoryName(System.Reflection
            .Assembly.GetExecutingAssembly().Location) ?? Directory.GetCurrentDirectory();
        return Path.Combine(exePath, fileName);
    }
    
}