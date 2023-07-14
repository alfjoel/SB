namespace SB.Cardnet;

public enum CncError
{
    Timeout,
    UnexpectedError,
    UnexpectedResponse,
    BadResponse,
    BadLrc,
    BadConnection,
    CloseConnection,
    UnknownTransaction,
    InvalidFunction,
    NoProgressTransaction,
    EmptyLots,
    ObjectNotFound,
    DuplicateObject,
    BadData
}