using System;
using System.Collections.Generic;

namespace S1_R3_R4_AT2.Models;

public partial class Produto
{
    public Guid ProdutoId { get; set; }

    public string? Nome { get; set; }

    public decimal Valor { get; set; }

    public Guid CategoriaId { get; set; }

    public DateTime DataCad { get; set; }

    public virtual Categoria Categoria { get; set; } = null!;
}
