using System.Text;

namespace SB.Cardnet;

public static class Utilities
{
    internal static List<byte> TrimArray(IEnumerable<byte> data)
    {
        var result = new List<byte>();

        // Remove "start of text -- SOT" indicator
        result.AddRange(data.Skip(1));

        // Remove "end of text -- EOT" indicator
        result.RemoveAt(result.Count - 1);

        return result;
    }
    
    internal static readonly Func<string, string> StringConverter = str => str.Trim();
    public static int FromDecimalToInt(decimal value, int digits)
    {
        var mult = (decimal)Math.Pow(10.0, digits);

        var roundedValue = Math.Round(value, digits);

        var result = (int)Math.Round(mult * roundedValue, digits);

        return result;
    }

    public static decimal FromIntToDecimal(long value, int digits)
    {
        var mult = (decimal)Math.Pow(10.0, digits);

        var result = Math.Round(value / mult, digits);

        return result;
    }

    private static byte GetLrc(byte[] data)
    {
        byte lrc = 0;
        for (var i = 1; i < data.Length; i++) lrc ^= data[i];
        return lrc;
    }

    internal static bool CheckLrc(byte[] data, byte lrc)
    {
        return GetLrc(data) == lrc;
    }

    internal static byte[] AddLrc(byte[] data)
    {
        var lcr = GetLrc(data);

        var result = new byte[data.Length + 1];

        data.CopyTo(result, 0);
        result[data.Length] = lcr;

        return result;
    }

    internal static void AddElementToArray<TType>(ref List<byte> dst, int valueLength, TType value,
        bool isLast = false)
    {
        const string errorMessage = "El valor {0} excede los limites permitidos";
        string section;

        switch (value)
        {
            case int:
            case long:
            {
                var strValue = value.ToString();

                // If value exceeds it's max length notify it
                if (strValue == null || strValue.Length > valueLength)
                    throw new CncException(CncError.BadData, string.Format(errorMessage, value));

                var complement = new string('0', valueLength - strValue.Length);

                section = complement + strValue;
                break;
            }
            case string:
            {
                var strValue = value.ToString();


                // If value exceeds it's max length notify it
                if (strValue == null || strValue.Length > valueLength)
                    throw new CncException(CncError.BadData, String.Format(errorMessage, value));

                var complement = new string(' ', valueLength - strValue.Length);

                section = value + complement;
                break;
            }
            default:
                section = "";
                break;
        }

        // Add separator to section if necessary
        if (!isLast && section.Length > 0)
        {
            dst.AddRange(Encoding.ASCII.GetBytes(section));
            dst.Add(Annotators.FieldSeparator);
        }
        else
        {
            dst.AddRange(Encoding.ASCII.GetBytes(section));
        }
    }

    internal static TType GetElementFromArray<TType>(ref List<byte> src, int valueLength, TType defaultValue,
        Func<string, TType> converter)
    {
        if (!(src.Count > 0) || src.Count < valueLength)
            return defaultValue;

        var indx = src.FindIndex(e => e == Annotators.FieldSeparator);

        if (indx == -1)
        {
            if (src.Count != valueLength) return defaultValue;
            var section = src.ToArray();
            src.RemoveRange(0, valueLength);
            return converter(Encoding.ASCII.GetString(section));
        }

        if (indx != valueLength) return defaultValue;
        {
            var section = src.Take(indx).ToArray();
            src.RemoveRange(0, indx + 1);
            return converter(Encoding.ASCII.GetString(section));
        }
    }
}