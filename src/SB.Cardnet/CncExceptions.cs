namespace SB.Cardnet;

internal static class CncExceptions
{
    private static readonly Dictionary<string, CncError> _errorCodes = new Dictionary<string, CncError>
    {
        { "40", CncError.InvalidFunction },
        { Defaults.NoProgressTransaction, CncError.NoProgressTransaction },
        { "10", CncError.EmptyLots },
        { "97", CncError.BadConnection }
    };

    private static readonly Dictionary<CncError, string> _errorMessages = new Dictionary<CncError, string>
    {
        { CncError.InvalidFunction, "La transaccion no es valida" },
        { CncError.NoProgressTransaction, "La transaccion no produjo una respuesta" },
        { CncError.EmptyLots, "Lotes vacios" },
        { CncError.BadConnection, "Revise conexion a internet" }
    };

    /// <summary>
    /// Returns an exception given an error code
    /// </summary>
    public static CncException GetException(string errorCode, string message = "")
    {
        try
        {
            var e = _errorCodes[errorCode];

            if (_errorMessages.ContainsKey(e))
                message = _errorMessages[e];

            return new CncException(e, message);
        }
        catch (KeyNotFoundException)
        {
            return null;
        }
    }
}