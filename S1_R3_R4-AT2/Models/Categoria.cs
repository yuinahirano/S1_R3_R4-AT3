using System;
using System.Collections.Generic;

namespace S1_R3_R4_AT2.Models;

public partial class Categoria
{
    public Guid CategoriaId { get; set; }

    public string Nome { get; set; } = null!;

    public string Descricao { get; set; } = null!;

    public DateTime DataCad { get; set; }

    public virtual ICollection<Produto> Produtos { get; set; } = new List<Produto>();
}
