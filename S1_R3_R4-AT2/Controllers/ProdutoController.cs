using Microsoft.AspNetCore.Mvc;
using S1_R3_R4_AT2.Context;
using S1_R3_R4_AT2.DTOs;
using S1_R3_R4_AT2.Models;

namespace S1_R3_R4_AT2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        MainContext ctx = new MainContext();

        [HttpGet]
        public IActionResult GetAllProdutos()
        {
            try
            {
                return Ok(ctx.Produtos);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno", detalhe = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            Produto produtos = ctx.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if (produtos == null)
                return NotFound();

            return Ok(produtos);
        }

        [HttpPost]
        public IActionResult CreateProduto(Produto produto)
        {
            try
            {
                ctx.Produtos.Add(produto);
                ctx.SaveChanges();

                return Created();

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno", detalhe = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduto(Guid id, ProdutoUpdateDTO produto)
        {
            try
            {
                var produtoBanco = ctx.Produtos.FirstOrDefault(p => p.ProdutoId == id);

                if (produtoBanco == null)
                    return NotFound();

                if (produto.Nome != null && produto.Nome.Any(char.IsDigit))
                    produtoBanco.Nome = produto.Nome;

                if (produto.Valor != null)
                    produtoBanco.Valor = produto.Valor.Value;

                if (produto.CategoriaId != null)
                    produtoBanco.CategoriaId = produto.CategoriaId.Value;

                ctx.Produtos.Update(produtoBanco);
                ctx.SaveChanges();
                return Ok(produtoBanco);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno", detalhe = ex.Message });
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduto(Guid id)
        {
            try
            {
                Produto produto = ctx.Produtos.FirstOrDefault(p => p.ProdutoId == id);

                if (produto == null)
                    return NotFound();

                ctx.Produtos.Remove(produto);
                ctx.SaveChanges();
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno", detalhe = ex.Message });
            }
        }
    }
}
