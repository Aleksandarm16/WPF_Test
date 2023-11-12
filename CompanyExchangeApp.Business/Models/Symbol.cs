using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using SQLite;

namespace CompanyExchangeApp.Business.Models;

public partial class Symbol
{
    [SQLite.PrimaryKey, AutoIncrement]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Ticker { get; set; } = null!;

    public string? Isin { get; set; }

    public string? CurrencyCode { get; set; }

    public DateOnly DateAdded { get; set; }

    public double? Price { get; set; }

    public DateOnly PriceDate { get; set; }

    public long TypeId { get; set; }

    public long ExchangeId { get; set; }

    public virtual Exchange Exchange { get; set; } = null!;

    public virtual Type Type { get; set; } = null!;
}
