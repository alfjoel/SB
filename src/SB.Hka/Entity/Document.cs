namespace SB.Hka.Entity;

internal class Document
{
    public NcfInfoCustomer? Ncf { get; set; } = null;
    public CustomerVatId? VatId { get; set; } = null;
    public CustomerName? Name { get; set; } = null;

    public DocumentType? Type { get; set; }
    public ArraySegment<InfoCustomer> Info { get; set; } = ArraySegment<InfoCustomer>.Empty;
    public InfoComment? Comment { get; set; } = null;
    private List<Detail> Details { get; set; } = new();

    public void AddDetail(Detail value)
    {
        if (value.Price < 0)
        {
            var last = Details.LastOrDefault();
            if (last is null) return;
            last.Price += value.Price;
            return;
        }

        Details.Add(value);
    }

    public bool CheckDetails()
    {
        return Details.Sum(t => t.Price).Equals(Decimal.Zero);
    }


    public IEnumerable<string> GeneratorCmd()
    {
        if (Name != null) yield return Name.ToSend();
        if (VatId != null) yield return VatId.ToSend();
        if (Ncf != null) yield return Ncf.ToSend();
        if (Type != null) yield return Type.ToSend();
        if (Info.Count > 0)
        {
            foreach (var info in Info.OrderBy(t => t.Line))
            {
                yield return info.ToSend();
            }
        }

        if (Comment != null) yield return Comment.ToSend();
        if (Details.Count > 0)
        {
            foreach (var info in Details)
            {
                yield return info.ToSend();
            }
        }

        yield return Constants.SubtotalImpreso;
        yield return Constants.PagoDirecto;
        yield return Constants.ClosetDocument;
    }
}