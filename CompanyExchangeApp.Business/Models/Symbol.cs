using System;
using System.Collections.Generic;

namespace CompanyExchangeApp.Business.Models;

public partial class Symbol
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Ticker { get; set; } = null!;

    public string? Isin { get; set; }

    public string? CurrencyCode { get; set; }

    public byte[]? DateAdded { get; set; }

    public double? Price { get; set; }

    public byte[]? PriceDate { get; set; }

    public long TypeId { get; set; }

    public long ExchangeId { get; set; }

    public virtual Exchange Exchange { get; set; } = null!;

    public virtual Type Type { get; set; } = null!;
}
