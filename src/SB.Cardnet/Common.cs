using System.Text;

namespace SB.Cardnet;

public static class Common
{
    public static int FromDecimalToInt(decimal value, int digits)
    {
        var mult = (decimal)Math.Pow(10.0, digits);

        var roundedValue = Math.Round(value, digits);

        var result = (int)Math.Round(mult * roundedValue, digits);

        return result;
    }
    public static decimal FromIntToDecimal(long value, int digits)
    {
        var mult = (decimal) Math.Pow(10.0, digits);

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

        if (value is int || value is long)
        {
            string strValue = value.ToString();

            // If value exceeds it's max length notify it
            if (strValue.Length > valueLength)
                throw new CncException(CncError.BadData, string.Format(errorMessage, value));

            string complement = new string('0', valueLength - strValue.Length);

            section = complement + strValue;
        }
        else if (value is string)
        {
            string strValue = value.ToString();

            // If value exceeds it's max length notify it
            if (strValue.Length > valueLength)
                throw new CncException(CncError.BadData, String.Format(errorMessage, value));

            string complement = new string(' ', valueLength - strValue.Length);

            section = value + complement;
        }
        else
        {
            section = "";
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
}