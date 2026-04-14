using System;
using System.Collections.Generic;

namespace S1_R3_R4_AT2.Models;

public partial class Cliente
{
    public Guid ClienteId { get; set; }

    public string Nome { get; set; } = null!;

    public string Cpf { get; set; } = null!;

    public DateTime DataCad { get; set; }

    public virtual ICollection<Endereco> Enderecos { get; set; } = new List<Endereco>();

    public virtual ICollection<Telefone> Telefones { get; set; } = new List<Telefone>();
}
