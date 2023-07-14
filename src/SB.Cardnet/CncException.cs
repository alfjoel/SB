namespace SB.Cardnet;

public class CncException : Exception
{
    public CncError ErrorCode { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    public CncException(CncError errorCode)
    {
        ErrorCode = errorCode;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public CncException(CncError errorCode, string message)
        : base(message)
    {
        ErrorCode = errorCode;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public CncException(CncError errorCode, string message, Exception inner)
        : base(message, inner)
    {
        ErrorCode = errorCode;
    }
}