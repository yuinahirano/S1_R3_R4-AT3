using System;
using System.Collections.Generic;

namespace S1_R3_R4_AT2.Models;

public partial class Telefone
{
    public Guid TelefoneId { get; set; }

    public string Telefone1 { get; set; } = null!;

    public Guid ClienteId { get; set; }

    public virtual ClienteUpdateDTO Cliente { get; set; } = null!;
}
