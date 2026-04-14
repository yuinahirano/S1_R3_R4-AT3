using System;
using System.Collections.Generic;

namespace S1_R3_R4_AT2.Models;

public partial class Endereco
{
    public Guid EnderecoId { get; set; }

    public string Cep { get; set; } = null!;

    public string Uf { get; set; } = null!;

    public string Cidade { get; set; } = null!;

    public string Bairro { get; set; } = null!;

    public string Complemento { get; set; } = null!;

    public string Logradouro { get; set; } = null!;

    public int Numero { get; set; }

    public Guid ClienteId { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;
}
