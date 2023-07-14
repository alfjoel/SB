using SB.Cardnet.Interfaces;
using SB.Infrastructure.Interfaces;

namespace SB.Cardnet;

public static class Services
{
    public static Response GetCardInfo(IClient stream, bool waitForEOT = Defaults.WaitForEOT)
    {
        var payload = new List<byte>();

        // TODO: Maybe move this to a real transaction object
        payload.Add(Annotators.StartOfText);
        Utilities.AddElementToArray<string>(ref payload, 4, TransactionTypes.CardInfo, isLast: true);
        payload.Add(Annotators.EndOfText);
        stream.Send(Utilities.AddLrc(payload.ToArray()));
        var requestResponse = stream.Receive(out var totalBytesRequestResponse, Timeouts.Approval);
        if (totalBytesRequestResponse == 0)
            throw new CncException(CncError.BadResponse, "La solicitud enviada al POS no produjo una respuesta");
        if (requestResponse[0] == Annotators.NegativeAcknowledge)
        {
            throw new CncException(CncError.BadLrc, "Respuesta enviada desde POS es corrupta o incompleta");
        }
        var response = stream.Receive(out var totalBytesReceived, Timeouts.Response);


        // await wStream.ReadAsync(Timeouts.Response, waitForEOT);
        // var response = wStream.GetResponse();

        // CheckForError(response, true);

        // return new CardInfoResponse(response);
    }
}