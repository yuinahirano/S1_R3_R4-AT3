using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S1_R3_R4_AT2.Context;
using S1_R3_R4_AT2.DTOs;
using S1_R3_R4_AT2.Models;

namespace S1_R3_R4_AT2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {

        MainContext ctx = new MainContext();

        [HttpGet]
        public IActionResult GetAllCategorias()
        {
            try
            {
                return Ok(ctx.Categorias);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno", detalhe = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                Categoria categoria = ctx.Categorias.FirstOrDefault(cat => cat.CategoriaId == id);

                if (categoria == null)
                    return NotFound();

                return Ok(categoria);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno", detalhe = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CreateCategoria(Categoria categoria)
        {
            try
            {
                ctx.Categorias.Add(categoria);
                ctx.SaveChanges();

                return Created();

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno", detalhe = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategoria(Guid id, CategoriaUpdateDTO categoria)
        {
            try
            {
                //categoria que esta salva no banco
                Categoria categoriaBanco = ctx.Categorias.FirstOrDefault(cat => cat.CategoriaId == id);

                //valida com dto pois permite não preencher todos os campos obrigatoriamente para atualizar
                if (categoriaBanco == null)
                    return NotFound();

                if (categoria.Nome != null && categoria.Nome.Any(char.IsDigit))
                    categoriaBanco.Nome = categoria.Nome;

                if (categoria.Descricao != null && categoria.Descricao.Any(char.IsDigit))
                    categoriaBanco.Descricao = categoria.Descricao;

                ctx.Categorias.Update(categoriaBanco);
                ctx.SaveChanges();
                return Ok(categoriaBanco);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno", detalhe = ex.Message });
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategoria(Guid id)
        {
            try
            {
                Categoria categoria = ctx.Categorias.FirstOrDefault(cat => cat.CategoriaId == id);

                if (categoria == null)
                    return NotFound();

                ctx.Categorias.Remove(categoria);
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
