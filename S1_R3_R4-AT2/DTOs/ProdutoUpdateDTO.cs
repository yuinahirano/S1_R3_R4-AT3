namespace S1_R3_R4_AT2.DTOs
{
    public class ProdutoUpdateDTO
    {
        public string? Nome { get; set; }

        public decimal? Valor { get; set; }

        public Guid? CategoriaId { get; set; }

        public DateTime? DataCad { get; set; }

    }
}
