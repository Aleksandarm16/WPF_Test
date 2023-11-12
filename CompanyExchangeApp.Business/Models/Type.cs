using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompanyExchangeApp.Business.Models;

public partial class Type
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Symbol> Symbols { get; set; } = new List<Symbol>();
}
